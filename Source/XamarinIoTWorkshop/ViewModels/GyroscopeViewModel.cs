using System.Threading.Tasks;

using Xamarin.Essentials;

namespace XamarinIoTWorkshop
{
    public class GyroscopeViewModel : ThreeAxisViewModel
    {
        #region Methods
        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            try
            {
                Gyroscope.Start(SensorSpeed.Fastest);
                Gyroscope.ReadingChanged += HandleGyroscopeReadingChanged;
            }
            catch (FeatureNotSupportedException)
            {
                OnFeatureNotSupportedExceptionThrown(typeof(Gyroscope));
            }
        }

        protected override void StopDataCollection()
        {
            base.StopDataCollection();

            try
            {
                Gyroscope.Stop();
                Gyroscope.ReadingChanged -= HandleGyroscopeReadingChanged;
            }
            catch (FeatureNotSupportedException)
            {

            }
        }

        async void HandleGyroscopeReadingChanged(GyroscopeChangedEventArgs e)
        {
            UpdateAxisValues(e.Reading.AngularVelocity);

            var gyroscopeData = new GyroscopeDataModel
            {
                GyroscopeX = e.Reading.AngularVelocity.X,
                GyroscopeY = e.Reading.AngularVelocity.Y,
                GyroscopeZ = e.Reading.AngularVelocity.Z,
            };

            await IoTDeviceService.SendMessage(gyroscopeData).ConfigureAwait(false);
        }
        #endregion
    }
}
