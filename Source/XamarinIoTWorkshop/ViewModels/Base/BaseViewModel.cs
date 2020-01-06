using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using AsyncAwaitBestPractices;
using Xamarin.Forms;

using XamarinIoTWorkshop.Shared;

namespace XamarinIoTWorkshop
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        readonly WeakEventManager _propertyChangedEventHandler = new WeakEventManager();
        readonly WeakEventManager<Type> _featureNotSupportedExceptionThrownEventHandler = new WeakEventManager<Type>();
        readonly WeakEventManager<bool> _dataCollectionStatusChangedEventHandler = new WeakEventManager<bool>();

        bool _isDataCollectionActive;
        string _dataCollectionButtonText = ButtonTextConstants.EndDataCollectionText;
        ICommand? _dataCollectionButtonCommand;

        protected BaseViewModel()
        {
            IoTDeviceService.IoTDeviceServiceFailed += HandleIoTDeviceServiceFailed;
            StartDataCollection();
        }

        public event EventHandler<bool> DataCollectionStatusChanged
        {
            add => _dataCollectionStatusChangedEventHandler.AddEventHandler(value);
            remove => _dataCollectionStatusChangedEventHandler.RemoveEventHandler(value);
        }

        public event EventHandler<Type> FeatureNotSupportedExceptionThrown
        {
            add => _featureNotSupportedExceptionThrownEventHandler.AddEventHandler(value);
            remove => _featureNotSupportedExceptionThrownEventHandler.RemoveEventHandler(value);
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => _propertyChangedEventHandler.AddEventHandler(value);
            remove => _propertyChangedEventHandler.RemoveEventHandler(value);
        }

        public ICommand DataCollectionButtonCommand => _dataCollectionButtonCommand ??= new Command(ExecuteDataCollectionButtonCommand);

        public string DataCollectionButtonText
        {
            get => _dataCollectionButtonText;
            set => SetProperty(ref _dataCollectionButtonText, value);
        }

        protected bool IsDataCollectionActive
        {
            get => _isDataCollectionActive;
            private set => SetProperty(ref _isDataCollectionActive, value, () => OnDataCollectionStatusChanged(value));
        }

        protected virtual void StartDataCollection() => IsDataCollectionActive = true;

        protected virtual void StopDataCollection() => IsDataCollectionActive = false;

        protected void SetProperty<T>(ref T backingStore, in T value, in Action? onChanged = null, [CallerMemberName] in string propertyname = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyname);
        }

        protected virtual void HandleIoTDeviceServiceFailed(object sender, string message)
        {
            StopDataCollection();
            DataCollectionButtonText = ButtonTextConstants.BeginDataCollectionText;
        }

        protected void OnFeatureNotSupportedExceptionThrown(Type xamarinEssentialsType)
        {
            AppCenterService.TrackEvent("Feature Not Supported Exception Thrown", "Type", xamarinEssentialsType.Name);
            _featureNotSupportedExceptionThrownEventHandler.HandleEvent(this, xamarinEssentialsType, nameof(FeatureNotSupportedExceptionThrown));
        }

        void ExecuteDataCollectionButtonCommand()
        {
            if (DataCollectionButtonText.Equals(ButtonTextConstants.BeginDataCollectionText))
            {
                AppCenterService.TrackEvent("Data Collection Button Tapped", "Button Text", ButtonTextConstants.BeginDataCollectionText);

                StartDataCollection();
                DataCollectionButtonText = ButtonTextConstants.EndDataCollectionText;
            }
            else
            {
                AppCenterService.TrackEvent("Data Collection Button Tapped", "Button Text", ButtonTextConstants.EndDataCollectionText);

                StopDataCollection();
                DataCollectionButtonText = ButtonTextConstants.BeginDataCollectionText;
            }
        }

        void OnDataCollectionStatusChanged(bool isCollectingData)
        {
            AppCenterService.TrackEvent("Data Collection Changed", "IsEnabled", isCollectingData.ToString());
            _dataCollectionStatusChangedEventHandler.HandleEvent(this, isCollectingData, nameof(DataCollectionStatusChanged));
        }

        void OnPropertyChanged([CallerMemberName]string name = "") => _propertyChangedEventHandler.HandleEvent(this, new PropertyChangedEventArgs(name), nameof(INotifyPropertyChanged.PropertyChanged));
    }
}
