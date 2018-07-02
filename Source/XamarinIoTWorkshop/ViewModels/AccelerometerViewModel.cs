using Xamarin.Essentials;

namespace XamarinIoTWorkshop
{
    public class AccelerometerViewModel : BaseViewModel
    {
        #region Fields
        double _xValue;
        #endregion

        #region Properties
        public double XValue
        {
            get => _xValue;
            set => SetProperty(ref _xValue, value);
        }
        #endregion

        #region Constructors
        public AccelerometerViewModel() => Accelerometer.ReadingChanged += HandleAccelerometerReadingChanged;
        #endregion

        #region Methods
        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            try
            {
                Accelerometer.Start(SensorSpeed.Normal);
            }
            catch (FeatureNotSupportedException)
            {
                OnFeatureNotSupportedExceptionThrown(typeof(Accelerometer));
            }
        }

        protected override void StopDataCollection()
        {
            base.StopDataCollection();

            try
            {
                Accelerometer.Stop();
            }
            catch (FeatureNotSupportedException)
            {

            }
        }

        async void HandleAccelerometerReadingChanged(AccelerometerChangedEventArgs e)
        {
            XValue = e.Reading.Acceleration.X;

            await IoTDeviceService.SendMessage(e.Reading).ConfigureAwait(false);
        }
        #endregion
    }
}
