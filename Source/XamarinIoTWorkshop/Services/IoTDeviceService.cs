using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;

using Newtonsoft.Json;

namespace XamarinIoTWorkshop
{
    public static class IoTDeviceService
    {
        #region Fields
        static DeviceClient _deviceClient;
        #endregion

        #region Constructors
        static IoTDeviceService() => IotHubSettings.DeviceConnectionStringChanged += HandleDeviceConnectionStringChanged;
        #endregion

        #region Events
        public static event EventHandler<string> IoTDeviceServiceFailed;
        #endregion

        #region Methods
        public static async Task SendMessage<T>(T data) where T : class
        {
            if (data is null)
                return;

            try
            {
                var jsonData = await Task.Run(() => JsonConvert.SerializeObject(data)).ConfigureAwait(false);

                var eventMessage = new Microsoft.Azure.Devices.Client.Message(Encoding.UTF8.GetBytes(jsonData));

                await SendEvent(eventMessage).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine($"Error sending the message: {e.Message}");
                OnIoTDeviceServiceFailed(e.Message);
            }
        }

        static DeviceClient GetDeviceClient() => _deviceClient ??
            (_deviceClient = DeviceClient.CreateFromConnectionString(IotHubSettings.DeviceConnectionString));

        static Task SendEvent(Microsoft.Azure.Devices.Client.Message eventMessage)
        {
            var deviceClient = GetDeviceClient();

            if (IotHubSettings.IsSendDataToAzureEnabled)
                return deviceClient.SendEventAsync(eventMessage);

            return Task.CompletedTask;
        }

        static void OnIoTDeviceServiceFailed(string message) => IoTDeviceServiceFailed?.Invoke(null, message);

        static void HandleDeviceConnectionStringChanged(object sender, EventArgs e) => _deviceClient = null;
        #endregion
    }
}