using System;
using System.Threading.Tasks;
using System.Windows.Input;

using AsyncAwaitBestPractices.MVVM;

using Xamarin.Essentials;
using Xamarin.Forms;

using XamarinIoTWorkshop.Shared;

namespace XamarinIoTWorkshop
{
    public class GeolocationViewModel : BaseViewModel
    {
        #region Fields
        Location _mostRecentLocation;
        ICommand _startGeolocationCommand, _sendMessage;
        #endregion

        #region Events
        public event EventHandler<Location> LocationUpdated;
        #endregion

        #region Properties
        ICommand StartGeolocationCommand => _startGeolocationCommand ??
            (_startGeolocationCommand = new AsyncCommand(StartGeolocationDataCollection, continueOnCapturedContext: false));

        ICommand SendMessage => _sendMessage ??
            (_sendMessage = new AsyncCommand<Location>(location =>
            {
                var geolocationData = new GeolocationDataModel
                {
                    Latitude = location.Latitude,
                    Longitude = location.Longitude
                };

                return IoTDeviceService.SendMessage(geolocationData);
            }, continueOnCapturedContext: false));

        Location MostRecentLocation
        {
            get => _mostRecentLocation;
            set
            {
                var milesTraveled = Location.CalculateDistance(_mostRecentLocation, value, DistanceUnits.Miles);

                if (milesTraveled > 0.5)
                {
                    _mostRecentLocation = value;
                    OnLocationUpdated(value);
                }
            }
        }
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
                    MostRecentLocation = await GeolocationService.GetLocation().ConfigureAwait(false);

                    SendMessage?.Execute(MostRecentLocation);

                    await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    AppCenterService.Report(e);

                    StopDataCollection();
                    DataCollectionButtonText = ButtonTextConstants.BeginDataCollectionText;
                }

            } while (IsDataCollectionActive);
        }

        void OnLocationUpdated(Location location) => LocationUpdated?.Invoke(this, location);
        #endregion
    }
}
