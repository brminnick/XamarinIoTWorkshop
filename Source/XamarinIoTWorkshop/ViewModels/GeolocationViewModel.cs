using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinIoTWorkshop.Shared;

namespace XamarinIoTWorkshop
{
    class GeolocationViewModel : BaseViewModel
    {
        readonly WeakEventManager<Location> _locationUpdatedEventManager = new WeakEventManager<Location>();

        Location? _mostRecentLocation;

        public event EventHandler<Location> LocationUpdated
        {
            add => _locationUpdatedEventManager.AddEventHandler(value);
            remove => _locationUpdatedEventManager.RemoveEventHandler(value);
        }

        Location MostRecentLocation
        {
            get => _mostRecentLocation ?? new Location(0, 0);
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

        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            StartGeolocationDataCollection().SafeFireAndForget();
        }

        async Task StartGeolocationDataCollection()
        {
            do
            {
                try
                {
                    MostRecentLocation = await GeolocationService.GetLocation().ConfigureAwait(false);

                    var geolocationData = new GeolocationDataModel(MostRecentLocation.Latitude, MostRecentLocation.Longitude);
                    IoTDeviceService.SendMessage(geolocationData).SafeFireAndForget();

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

        void OnLocationUpdated(Location location) => _locationUpdatedEventManager.HandleEvent(this, location, nameof(LocationUpdated));
    }
}
