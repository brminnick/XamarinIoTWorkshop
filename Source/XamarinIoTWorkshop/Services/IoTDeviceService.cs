using System;
using System.Text;
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
        const string _iotHubConnectionString = "HostName=XamarinIoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=ZAmvNEE0Ghp4wMSDdxj+CSgkhxlAKcKiLDqWTfCHkFs=";
        const string _endpointConnectionString = "Endpoint=sb://iothub-ns-xamariniot-494672-b1c0b6c176.servicebus.windows.net/;SharedAccessKeyName=iothubowner;SharedAccessKey=ZAmvNEE0Ghp4wMSDdxj+CSgkhxlAKcKiLDqWTfCHkFs=";
        readonly static Lazy<RegistryManager> _registryManagerHolder = new Lazy<RegistryManager>(() => RegistryManager.CreateFromConnectionString(_iotHubConnectionString));
        #endregion

        #region Fields
        static Device _device;
        static DeviceClient _deviceClient;
        #endregion

        #region Events
        public static event EventHandler<string> IoTDeviceServiceFailed;
        #endregion

        #region Properties
        static RegistryManager RegistryManager => _registryManagerHolder.Value;
        #endregion

        #region Methods
        public static async Task SendMessage<T>(T data)
        {
            try
            {
                if (_device is null)
                    _device = await AddDeviceAsync().ConfigureAwait(false);

                var jsonData = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

                var eventMessage = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(jsonData));

                await SendEvent(eventMessage).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error retrieving the device: {e.Message}");
            }
        }

        static async Task<Device> AddDeviceAsync()
        {
            string deviceId = CrossDeviceInfo.Current.Id;

            try
            {
                var device = new Device(deviceId) { Status = DeviceStatus.Enabled };
                return await RegistryManager.AddDeviceAsync(device).ConfigureAwait(false);
            }
            catch (DeviceAlreadyExistsException)
            {
                try
                {
                    return await RegistryManager.GetDeviceAsync(deviceId).ConfigureAwait(false);
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