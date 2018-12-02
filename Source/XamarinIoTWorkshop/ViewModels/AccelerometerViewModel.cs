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
                Accelerometer.Start(SensorSpeed.Normal);
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

                var accelerometerData = new AccelerometerDataModel
                {
                    AccelerometerX = XAxisValue,
                    AccelerometerY = YAxisValue,
                    AccelerometerZ = ZAxisValue
                };

                await IoTDeviceService.SendMessage(accelerometerData).ConfigureAwait(false);
            }
        }

        void HandleAccelerometerReadingChanged(object sender, AccelerometerChangedEventArgs e) => UpdateAxisValues(e.Reading.Acceleration);
        #endregion
    }
}
