using System;

namespace XamarinIoTWorkshop
{
    public abstract class IotHubSettings : BaseSettings
    {
        #region Fields
        static string _deviceConnectionString;
        static bool _isSendDataToAzureEnabled;
        #endregion

        #region Events
        public static event EventHandler DeviceConnectionStringChanged;
        #endregion

        #region Properties
        public static string DeviceConnectionString
        {
            get => GetSetting(ref _deviceConnectionString);
            set
            {
                SetSetting(ref _deviceConnectionString, value);
                DeviceConnectionStringChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        public static bool IsSendDataToAzureEnabled
        {
            get => GetSetting(ref _isSendDataToAzureEnabled);
            set => SetSetting(ref _isSendDataToAzureEnabled, value);
        }
        #endregion
    }
}
