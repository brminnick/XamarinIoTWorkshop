using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public static class GeolocationService
    {
        static readonly WeakEventManager<Exception> _geolocationFailedEventManager = new WeakEventManager<Exception>();
        static readonly Lazy<GeolocationRequest> _geolocationRequestHolder = new Lazy<GeolocationRequest>(() => new GeolocationRequest(GeolocationAccuracy.Best));

        public static event EventHandler<Exception> GeolocationFailed
        {
            add => _geolocationFailedEventManager.AddEventHandler(value);
            remove => _geolocationFailedEventManager.RemoveEventHandler(value);
        }

        static GeolocationRequest GeolocationRequest => _geolocationRequestHolder.Value;

        public static Task<Location> GetLocation()
        {
            return Device.InvokeOnMainThreadAsync(async () =>
            {
                try
                {
                    return await Geolocation.GetLocationAsync(GeolocationRequest).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    OnGeolocationFailed(e);
                    AppCenterService.Report(e);

                    throw;
                }
            });
        }

        static void OnGeolocationFailed(Exception exception) => _geolocationFailedEventManager.HandleEvent(null, exception, nameof(GeolocationFailed));
    }
}
