using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinIoTWorkshop
{
    public class AccelerometerViewModel : ThreeAxisViewModel
    {
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
            UpdateAxisValues(e.Reading.Acceleration);

            await IoTDeviceService.SendMessage(e.Reading).ConfigureAwait(false);
        }
        #endregion
    }
}
