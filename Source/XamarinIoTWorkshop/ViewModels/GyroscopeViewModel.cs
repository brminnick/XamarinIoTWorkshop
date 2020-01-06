using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    class GyroscopeViewModel : ThreeAxisViewModel
    {
        public GyroscopeViewModel() => Device.StartTimer(TimeSpan.FromSeconds(1), SendData);

        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            try
            {
                Gyroscope.Start(SensorSpeed.Default);
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

        protected override async Task SendIoTData()
        {
            if (IsDataCollectionActive)
            {
                var accelerometerData = new GyroscopeDataModel(XAxisValue, YAxisValue, ZAxisValue);
                await IoTDeviceService.SendMessage(accelerometerData).ConfigureAwait(false);
            }
        }

        void HandleGyroscopeReadingChanged(object sender, GyroscopeChangedEventArgs e) => UpdateAxisValues(e.Reading.AngularVelocity);
    }
}
