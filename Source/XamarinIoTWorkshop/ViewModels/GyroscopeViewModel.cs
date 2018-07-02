using System.Threading.Tasks;

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
                Gyroscope.Start(SensorSpeed.Normal);
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

            System.Diagnostics.Debug.WriteLine($"Angualr Velocity X: {e.Reading.AngularVelocity.X}");
            System.Diagnostics.Debug.WriteLine($"Angualr Velocity Y: {e.Reading.AngularVelocity.Y}");
            System.Diagnostics.Debug.WriteLine($"Angualr Velocity Z: {e.Reading.AngularVelocity.Z}");

            await IoTDeviceService.SendMessage(e.Reading).ConfigureAwait(false);
        }
        #endregion
    }
}
