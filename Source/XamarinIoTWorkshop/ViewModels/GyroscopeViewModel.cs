using Xamarin.Essentials;

namespace XamarinIoTWorkshop
{
    public class GyroscopeViewModel : ThreeAxisViewModel
    {
        #region Constructors
        public GyroscopeViewModel() => Gyroscope.ReadingChanged += HandleGyroscopeReadingChanged;
        #endregion

        #region Methods
        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            try
            {
                Gyroscope.Start(SensorSpeed.Fastest);
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
            }
            catch (FeatureNotSupportedException)
            {

            }
        }

        async void HandleGyroscopeReadingChanged(GyroscopeChangedEventArgs e)
        {
            UpdateAxisValues(e.Reading.AngularVelocity);

            await IoTDeviceService.SendMessage(e.Reading).ConfigureAwait(false);
        }
        #endregion
    }
}
