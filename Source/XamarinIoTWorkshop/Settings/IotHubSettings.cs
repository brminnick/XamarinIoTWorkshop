namespace XamarinIoTWorkshop
{
    public abstract class IotHubSettings : BaseSettings
    {
        #region Fields
        static string _iotHubConnectionString;
        #endregion

        #region Properties
        public static string IotHubConnectionString
        {
            get => GetSetting(ref _iotHubConnectionString);
            set => SetSetting(ref _iotHubConnectionString, value);
        }
        #endregion
    }
}
