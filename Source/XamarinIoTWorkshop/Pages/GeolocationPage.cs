using System;
using Xamarin.Essentials;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class GeolocationPage : BaseContentPage<GeolocationViewModel>
    {
        readonly Xamarin.Forms.Maps.Map _map;
        readonly Distance _distance = Distance.FromMiles(5);

        public GeolocationPage()
        {
            Icon = "Geolocation";
            Title = "Geolocation";

            _map = new Xamarin.Forms.Maps.Map { IsShowingUser = true };

            ViewModel.LocationUpdated += HandleLocationUpdated;
            GeolocationService.GeolocationFailed += HandleGeolocationFailed;
            ViewModel.DataCollectionStatusChanged += HandleDataCollectionChanged;

            Content = _map;

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(false);
        }

        void HandleDataCollectionChanged(object sender, bool isCollectingData) =>
            Device.BeginInvokeOnMainThread(() => _map.IsShowingUser = isCollectingData);

        void HandleGeolocationFailed(object sender, Exception e) =>
            Device.BeginInvokeOnMainThread(async () => await DisplayAlert("Error", e.Message, "OK"));

        void HandleLocationUpdated(object sender, Location e) =>
            Device.BeginInvokeOnMainThread(() => _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(e.Latitude, e.Longitude), _distance)));
    }
}
