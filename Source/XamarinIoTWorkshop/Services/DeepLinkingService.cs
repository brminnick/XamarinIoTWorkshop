using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public static class DeepLinkingService
    {
        public static Task OpenApp(string deepLinkingUrl, string browserUrl)
        {
            if (MainThread.IsMainThread)
                return openApp(deepLinkingUrl, browserUrl);

            return Device.InvokeOnMainThreadAsync(() => openApp(deepLinkingUrl, browserUrl));

            static async Task openApp(string appUrl, string webUrl)
            {
                var supportsUri = await Launcher.CanOpenAsync(appUrl);

                if (supportsUri)
                {
                    AppCenterService.TrackEvent("Launched Twitter Profile", "Method", "Twitter App");
                    await Launcher.OpenAsync(appUrl);
                }
                else
                {
                    AppCenterService.TrackEvent("Launched Twitter Profile", "Method", "Browser");
                    await Browser.OpenAsync(webUrl);
                }
            }
        }
    }
}
