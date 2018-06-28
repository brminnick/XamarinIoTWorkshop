using System;
using Microsoft.Azure.Devices;

namespace XamarinIoTWorkshop
{
    public class IoTDeviceConfigurationModel
    {
        public string Nickname { get; set; }
        public string DeviceId { get; set; }
        public string GenerationId { get; set; }
        public string ETag { get; set; }
        public DeviceConnectionState ConnectionState { get; set; }
        public DeviceStatus Status { get; set; }
        public string StatusReason { get; set; }
        public DateTimeOffset ConnectionStateUpdatedTime { get; set; }
        public DateTimeOffset StatusUpdatedTime { get; set; }
        public DateTimeOffset LastActivityTime { get; set; }
        public int CloudToDeviceMessageCount { get; set; }
    }
}
