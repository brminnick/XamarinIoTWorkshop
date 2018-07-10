using System;
using System.Text;
using System.Threading.Tasks;

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

                var eventMessage = new Message(Encoding.UTF8.GetBytes(jsonData));

                await SendEvent(eventMessage).ConfigureAwait(false);

                AppCenterService.TrackEvent("IoT Message Sent", "Data Type", typeof(T).Name);
            }
            catch (Exception e)
            {
                AppCenterService.Report(e);
                OnIoTDeviceServiceFailed(e.Message);
            }
        }

        static DeviceClient GetDeviceClient() => _deviceClient ??
            (_deviceClient = DeviceClient.CreateFromConnectionString(IotHubSettings.DeviceConnectionString));

        static Task SendEvent(Message eventMessage)
        {
            if (IotHubSettings.IsSendDataToAzureEnabled)
            {
                var deviceClient = GetDeviceClient();
                return deviceClient.SendEventAsync(eventMessage);
            }

            return Task.CompletedTask;
        }

        static void OnIoTDeviceServiceFailed(string message)
        {
            AppCenterService.TrackEvent("IoT Device Service Failed", "Message", message);
            IoTDeviceServiceFailed?.Invoke(null, message);
        }

        static void HandleDeviceConnectionStringChanged(object sender, EventArgs e) => _deviceClient = null;
        #endregion
    }
}