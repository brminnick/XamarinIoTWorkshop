using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Common.Exceptions;

using Newtonsoft.Json;

using Plugin.DeviceInfo;

namespace XamarinIoTWorkshop
{
    public static class IoTDeviceService
    {
        #region Constant Fields
        readonly static SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        #endregion

        #region Fields
        static Device _device;
        static DeviceClient _deviceClient;
        #endregion

        #region Events
        public static event EventHandler<string> IoTDeviceServiceFailed;
        #endregion

        #region Methods
        public static async Task SendMessage<T>(T data)
        {
            
            try
            {
                await _semaphore.WaitAsync().ConfigureAwait(false);

                if (_device is null)
                    _device = await AddDeviceAsync().ConfigureAwait(false);

                _semaphore.Release();

                var jsonData = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

                var eventMessage = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(jsonData));

                await SendEvent(eventMessage).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving the device: {e.Message}");
                OnIoTDeviceServiceFailed(e.Message);
            }
        }

        static async Task<Device> AddDeviceAsync()
        {
            RegistryManager registryManager;
            var connectionString = IotHubSettings.IotHubConnectionString;

            try
            {
                registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            }
            catch (FormatException e)
            {
                System.Diagnostics.Debug.WriteLine($"Invalid Connection String: {e.Message}");
                throw;
            }

            string deviceId = CrossDeviceInfo.Current.Id;

            try
            {
                var device = new Device(deviceId) { Status = DeviceStatus.Enabled };
                return await registryManager.AddDeviceAsync(device).ConfigureAwait(false);
            }
            catch (DeviceAlreadyExistsException)
            {
                try
                {
                    return await registryManager.GetDeviceAsync(deviceId).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine($"Error retrieving the device: {e.Message}");
                    throw;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating the device: {e.Message}");
                throw;
            }
        }

        static async ValueTask<DeviceClient> GetDeviceClient()
        {
            if (_device is null)
                _device = await AddDeviceAsync().ConfigureAwait(false);

            return _deviceClient ?? (_deviceClient = DeviceClient.CreateFromConnectionString(GetConnectionString(_device)));
        }

        static string GetConnectionString(Device device)
        {
            var connectionString = $"HostName={IoTConstants.HostName};DeviceId={device.Id};SharedAccessKey={device.Authentication.SymmetricKey.PrimaryKey}";

            return connectionString;
        }

        static async Task SendEvent(Microsoft.Azure.Devices.Client.Message eventMessage)
        {
            var deviceClient = await GetDeviceClient().ConfigureAwait(false);
            await deviceClient.SendEventAsync(eventMessage).ConfigureAwait(false);
        }

        static void OnIoTDeviceServiceFailed(string message) => IoTDeviceServiceFailed?.Invoke(null, message);
        #endregion
    }
}