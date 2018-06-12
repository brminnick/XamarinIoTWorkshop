using System;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Azure.Devices.Client;

using Newtonsoft.Json;

namespace XamarinIoTWorkshop
{
    public static class IoTDeviceService
    {
        #region Constant Fields
        const string _connectionString = "HostName=XamarinIoTHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=ZAmvNEE0Ghp4wMSDdxj+CSgkhxlAKcKiLDqWTfCHkFs=";
        readonly static Lazy<DeviceClient> _clientHolder = new Lazy<DeviceClient>(() => DeviceClient.CreateFromConnectionString(_connectionString));
        #endregion

        #region Properties
        static DeviceClient Client => _clientHolder.Value;
        #endregion

        #region Methods
        public static Task SendData<T>(T data)
        {
            var eventMessage = new Message(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)));

            return SendEvent(eventMessage);
        }

        static Task SendEvent(Message eventMessage) => Client.SendEventAsync(eventMessage);
        #endregion
    }
}