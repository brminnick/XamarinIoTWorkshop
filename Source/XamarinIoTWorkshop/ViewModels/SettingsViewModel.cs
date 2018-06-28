using System;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class SettingsViewModel : BaseViewModel
    {
        #region Constant Fields
        const string _beginDataCollectionText = "Begin Data Collection";
        const string _endDataCollectionText = "End Data Collection";
        #endregion

        #region Fields
        bool _isDataCollectionActive;
        string _dataCollectedLabelText = string.Empty;
        string _dataCollectionButtonText = _beginDataCollectionText;
        ICommand _dataCollectionButtonCommand;
        #endregion

        #region Constructors
        public SettingsViewModel()
        {
            Accelerometer.ReadingChanged += HandleAccelerometerReadingChanged;
            Gyroscope.ReadingChanged += HandleGyroscopeReadingChanged;
        }
        #endregion

        #region Events
        public event EventHandler<Type> FeatureNotSupportedExceptionThrown;
        #endregion

        #region Properties
        public ICommand DataCollectionButtonCommand => _dataCollectionButtonCommand ??
            (_dataCollectionButtonCommand = new Command(ExecuteDataCollectionButtonCommand));

        public string DataCollectionButtonText
        {
            get => _dataCollectionButtonText;
            set => SetProperty(ref _dataCollectionButtonText, value);
        }

        public string DataCollectedLabelText
        {
            get => _dataCollectedLabelText;
            set => SetProperty(ref _dataCollectedLabelText, value);
        }
        #endregion

        #region Methods
        void ExecuteDataCollectionButtonCommand()
        {
            if (DataCollectionButtonText.Equals(_beginDataCollectionText))
            {
                StartDataCollection();
                DataCollectionButtonText = _endDataCollectionText;
            }
            else
            {
                StopDataCollection();
                DataCollectionButtonText = _beginDataCollectionText;
            }
        }

        void StartDataCollection()
        {
            _isDataCollectionActive = true;

            StartGeolocationDataCollection();

            try
            {
                Accelerometer.Start(SensorSpeed.Normal);
            }
            catch (FeatureNotSupportedException)
            {
                OnFeatureNotSupportedExceptionThrown(typeof(Accelerometer));
            }

            try
            {
                Gyroscope.Start(SensorSpeed.Normal);
            }
            catch (FeatureNotSupportedException)
            {
                OnFeatureNotSupportedExceptionThrown(typeof(Gyroscope));
            }
        }

        void StopDataCollection()
        {
            _isDataCollectionActive = false;

            try
            {
                Accelerometer.Stop();
                Gyroscope.Stop();
            }
            catch (FeatureNotSupportedException)
            {

            }
        }

        async void StartGeolocationDataCollection()
        {
            do
            {
                var location = await GeolocationService.GetLocation().ConfigureAwait(false);

                if (location is null)
                    break;

                _dataCollectedLabelText = $"{_dataCollectedLabelText}\n Latitude: {location.Latitude}\n Longitude: {location.Longitude}\n";

                await IoTDeviceService.SendMessage(location).ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            } while (_isDataCollectionActive);
        }

        async void HandleAccelerometerReadingChanged(AccelerometerChangedEventArgs e)
        {
            UpdateDataCollectedLabel(e.Reading.Acceleration, "Accelerometer");

            await IoTDeviceService.SendMessage(e.Reading).ConfigureAwait(false);
        }

        async void HandleGyroscopeReadingChanged(GyroscopeChangedEventArgs e)
        {
            UpdateDataCollectedLabel(e.Reading.AngularVelocity, "Gyroscope");

            await IoTDeviceService.SendMessage(e.Reading).ConfigureAwait(false);
        }

        void UpdateDataCollectedLabel(Vector3 vector, string dataType)
        {
            var accelerometerTextBuilder = new StringBuilder();
            accelerometerTextBuilder.AppendLine($"{dataType} X: {vector.X}");
            accelerometerTextBuilder.AppendLine($"{dataType} Y: {vector.Y}");
            accelerometerTextBuilder.AppendLine($"{dataType} Z: {vector.Z}");

            _dataCollectedLabelText = $"{_dataCollectedLabelText} {accelerometerTextBuilder.ToString()}";
        }

        void OnFeatureNotSupportedExceptionThrown(Type xamarinEssentialsType) =>
            FeatureNotSupportedExceptionThrown?.Invoke(this, xamarinEssentialsType);
        #endregion
    }
}
