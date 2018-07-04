using System.Threading.Tasks;
using Xamarin.Essentials;

namespace XamarinIoTWorkshop
{
    public class AccelerometerViewModel : ThreeAxisViewModel
    {
        #region Methods
        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            try
            {
                Accelerometer.Start(SensorSpeed.Fastest);
                Accelerometer.ReadingChanged += HandleAccelerometerReadingChanged;
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
                Accelerometer.ReadingChanged -= HandleAccelerometerReadingChanged;
            }
            catch (FeatureNotSupportedException)
            {

            }
        }

        async void HandleAccelerometerReadingChanged(AccelerometerChangedEventArgs e)
        {
            UpdateAxisValues(e.Reading.Acceleration);

            var accelerometerData = new AccelerometerDataModel
            {
                AccelerometerX = e.Reading.Acceleration.X,
                AccelerometerY = e.Reading.Acceleration.Y,
                AccelerometerZ = e.Reading.Acceleration.Z,
            };

            await IoTDeviceService.SendMessage(accelerometerData).ConfigureAwait(false);
        }
        #endregion
    }
}
