using System;
using System.Threading.Tasks;

using Xamarin.Essentials;

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
		public static async Task<Location> GetLocation()
		{
			try
			{
				var location = await Geolocation.GetLocationAsync(GeolocationRequest).ConfigureAwait(false);

				return location;
			}
			catch (Exception e)
			{
				OnGeolocationFailed(e);
				return null;
			}
		}

		static void OnGeolocationFailed(Exception exception) => GeolocationFailed?.Invoke(null, exception);
		#endregion
	}
}
