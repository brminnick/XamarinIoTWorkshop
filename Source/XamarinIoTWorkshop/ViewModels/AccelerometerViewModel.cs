using System.Threading.Tasks;

using Xamarin.Essentials;

namespace XamarinIoTWorkshop
{
    class AccelerometerViewModel : ThreeAxisViewModel
    {
        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            try
            {
                Accelerometer.Start(SensorSpeed.Default);
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

        protected override async Task SendIoTData()
        {
            if (IsDataCollectionActive)
            {
                var accelerometerData = new AccelerometerDataModel(XAxisValue, YAxisValue, ZAxisValue);
                await IoTDeviceService.SendMessage(accelerometerData).ConfigureAwait(false);
            }
        }

        void HandleAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e) => UpdateAxisValues(e.Reading.Acceleration);
    }
}
