using System;
using System.Threading.Tasks;

using Xamarin.Essentials;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class GeolocationViewModel : BaseViewModel
    {
        #region Fields
        ICommand _startGeolocationCommand;
        #endregion

        #region Events
        public event EventHandler<Location> LocationUpdated;
        #endregion

        #region Properties
        ICommand StartGeolocationCommand => _startGeolocationCommand ??
            (_startGeolocationCommand = new Command(async () => await StartGeolocationDataCollection().ConfigureAwait(false)));
        #endregion

        #region Methods
        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            StartGeolocationCommand?.Execute(null);
        }

        async Task StartGeolocationDataCollection()
        {
            do
            {
                try
                {
                    var location = await GeolocationService.GetLocation().ConfigureAwait(false);

                    OnLocationUpdated(location);

                    await IoTDeviceService.SendMessage(location).ConfigureAwait(false);

                    await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                }
                catch (Exception)
                {
                    StopDataCollection();
                }

            } while (IsDataCollectionActive);
        }

        void OnLocationUpdated(Location location) => LocationUpdated?.Invoke(this, location);
        #endregion
    }
}
