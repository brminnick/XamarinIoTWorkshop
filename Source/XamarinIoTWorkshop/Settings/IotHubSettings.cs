using System;
using AsyncAwaitBestPractices;
using Xamarin.Essentials;

namespace XamarinIoTWorkshop
{
    public static class IotHubSettings
    {
        readonly static WeakEventManager _deviceConnectionStringChangedEventManager = new WeakEventManager();

        public static event EventHandler DeviceConnectionStringChanged
        {
            add => _deviceConnectionStringChangedEventManager.AddEventHandler(value);
            remove => _deviceConnectionStringChangedEventManager.RemoveEventHandler(value);
        }

        public static string DeviceConnectionString
        {
            get => Preferences.Get(nameof(DeviceConnectionString), string.Empty);
            set
            {
                Preferences.Set(nameof(DeviceConnectionString), value);
                _deviceConnectionStringChangedEventManager.HandleEvent(null, EventArgs.Empty, nameof(DeviceConnectionStringChanged));
            }
        }

        public static bool IsSendDataToAzureEnabled
        {
            get => Preferences.Get(nameof(IsSendDataToAzureEnabled), true);
            set => Preferences.Set(nameof(IsSendDataToAzureEnabled), value);
        }
    }
}
