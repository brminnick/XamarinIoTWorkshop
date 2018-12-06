using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class GyroscopeViewModel : ThreeAxisViewModel
    {
        #region Constructors
        public GyroscopeViewModel() => Device.StartTimer(TimeSpan.FromSeconds(1), SendData);
        #endregion

        #region Methods
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

        private void HandleGyroscopeReadingChanged(object sender, GyroscopeChangedEventArgs e) => UpdateAxisValues(e.Reading.AngularVelocity);

        protected override async Task SendIoTData()
        {
            if (IsDataCollectionActive)
            {

                var accelerometerData = new GyroscopeDataModel
                {
                    GyroscopeX = XAxisValue,
                    GyroscopeY = YAxisValue,
                    GyroscopeZ = ZAxisValue
                };

                await IoTDeviceService.SendMessage(accelerometerData).ConfigureAwait(false);
            }
        }
        #endregion

    }
}
