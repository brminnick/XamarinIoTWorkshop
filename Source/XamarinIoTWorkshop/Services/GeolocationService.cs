using System;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public static class GeolocationService
    {
        #region Constant Fields
        static readonly Lazy<GeolocationRequest> _geolocationRequestHolder = new Lazy<GeolocationRequest>(() => new GeolocationRequest(GeolocationAccuracy.Best));
        #endregion

        #region Events
        public static event EventHandler<Exception> GeolocationFailed;
        #endregion

        #region Properties
        static GeolocationRequest GeolocationRequest => _geolocationRequestHolder.Value;
        #endregion

        #region Methods
        public static Task<Location> GetLocation()
        {
            var tcs = new TaskCompletionSource<Location>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                try
                {
                    var location = await Geolocation.GetLocationAsync(GeolocationRequest).ConfigureAwait(false);
                    tcs.SetResult(location);
                }
                catch (Exception e)
                {
                    OnGeolocationFailed(e);
                    AppCenterService.Report(e);

                    tcs.SetException(e);
                }
            });

            return tcs.Task;
        }

        static void OnGeolocationFailed(Exception exception) => GeolocationFailed?.Invoke(null, exception);
        #endregion
    }
}
