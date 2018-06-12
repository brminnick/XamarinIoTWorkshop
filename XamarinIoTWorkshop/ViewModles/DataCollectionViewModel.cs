using System;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;
using System.Linq;
using System.Text;
using System.Numerics;

namespace XamarinIoTWorkshop
{
    public class DataCollectionViewModel : BaseViewModel
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

            Accelerometer.ReadingChanged += HandleAccelerometerReadingChanged;
            Gyroscope.ReadingChanged += HandleGyroscopeReadingChanged;

            StartGeolocationDataCollection();
        }

        void StopDataCollection()
        {
            _isDataCollectionActive = false;

            Accelerometer.ReadingChanged -= HandleAccelerometerReadingChanged;
            Gyroscope.ReadingChanged -= HandleGyroscopeReadingChanged;
        }
        
        async void StartGeolocationDataCollection()
        {
            do
            {
                var location = await GeolocationService.GetLocation().ConfigureAwait(false);

                if (location is null)
                    break;

                _dataCollectedLabelText = $"{_dataCollectedLabelText}\n Latitude: {location.Latitude}\n Longitude: {location.Longitude}\n";

                await IoTDeviceService.SendData(location).ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            } while (_isDataCollectionActive);
        }

        async void HandleAccelerometerReadingChanged(AccelerometerChangedEventArgs e)
        {
            UpdateDataCollectedLabel(e.Reading.Acceleration, "Accelerometer");

            await IoTDeviceService.SendData(e.Reading.Acceleration).ConfigureAwait(false);
        }

        async void HandleGyroscopeReadingChanged(GyroscopeChangedEventArgs e)
        {
            UpdateDataCollectedLabel(e.Reading.AngularVelocity, "Gyroscope");

            await IoTDeviceService.SendData(e.Reading.AngularVelocity).ConfigureAwait(false);
        }

        void UpdateDataCollectedLabel(Vector3 vector, string dataType)
        {
            var accelerometerTextBuilder = new StringBuilder();
            accelerometerTextBuilder.AppendLine($"{dataType} X: {vector.X}");
            accelerometerTextBuilder.AppendLine($"{dataType} Y: {vector.Y}");
            accelerometerTextBuilder.AppendLine($"{dataType} Z: {vector.Z}");

            _dataCollectedLabelText = $"{_dataCollectedLabelText} {accelerometerTextBuilder.ToString()}";
        }
        #endregion
    }
}
