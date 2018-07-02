using System;
using System.Threading.Tasks;

namespace XamarinIoTWorkshop
{
    public class GeolocationViewModel: BaseViewModel
    {
        protected override void StartDataCollection()
        {
            base.StartDataCollection();

            StartGeolocationDataCollection();
        }

        async void StartGeolocationDataCollection()
        {
            do
            {
                var location = await GeolocationService.GetLocation().ConfigureAwait(false);

                if (location is null)
                    break;

                await IoTDeviceService.SendMessage(location).ConfigureAwait(false);

                await Task.Delay(TimeSpan.FromSeconds(1)).ConfigureAwait(false);

            } while (IsDataCollectionActive);
        }
    }
}
