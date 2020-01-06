using System;
using System.Text;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Microsoft.Azure.Devices.Client;

using Newtonsoft.Json;

namespace XamarinIoTWorkshop
{
    public static class IoTDeviceService
    {
        readonly static WeakEventManager<string> _ioTDeviceServiceFailedEventManager = new WeakEventManager<string>();
        static DeviceClient? _deviceClient;

        static IoTDeviceService() => IotHubSettings.DeviceConnectionStringChanged += HandleDeviceConnectionStringChanged;

        public static event EventHandler<string> IoTDeviceServiceFailed
        {
            add => _ioTDeviceServiceFailedEventManager.AddEventHandler(value);
            remove => _ioTDeviceServiceFailedEventManager.RemoveEventHandler(value);
        }

        public static async Task SendMessage<T>(T data)
        {
            if (data is null)
                return;

            try
            {
                var jsonData = JsonConvert.SerializeObject(data);

                using var eventMessage = new Message(Encoding.UTF8.GetBytes(jsonData));

                await SendEvent(eventMessage).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                AppCenterService.Report(e);
                OnIoTDeviceServiceFailed(e.Message);
            }
        }

        static DeviceClient GetDeviceClient() => _deviceClient ??= DeviceClient.CreateFromConnectionString(IotHubSettings.DeviceConnectionString);

        static async ValueTask SendEvent(Message eventMessage)
        {
            if (!IotHubSettings.IsSendDataToAzureEnabled)
                return;

            var deviceClient = GetDeviceClient();
            await deviceClient.SendEventAsync(eventMessage).ConfigureAwait(false);

            AppCenterService.TrackEvent("IoT Message Sent");
        }

        static void OnIoTDeviceServiceFailed(string message)
        {
            AppCenterService.TrackEvent("IoT Device Service Failed", "Message", message);
            _ioTDeviceServiceFailedEventManager.HandleEvent(null, message, nameof(IoTDeviceServiceFailed));
        }

        static void HandleDeviceConnectionStringChanged(object sender, EventArgs e) => _deviceClient = null;
    }
}