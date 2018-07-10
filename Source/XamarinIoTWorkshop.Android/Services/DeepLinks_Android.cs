using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.Support.CustomTabs;

using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinIoTWorkshop.Droid.DeepLinks_Android))]
namespace XamarinIoTWorkshop.Droid
{
    public class DeepLinks_Android : IDeepLinks
    {
        Activity CurrentActivity => CrossCurrentActivity.Current.Activity;

        public Task OpenTwitter()
        {
            return Task.Run(() =>
            {
                var packageManager = CurrentActivity.PackageManager;

                try
                {
                    CurrentActivity.StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse(TwitterConstants.BrandonMinnickTwitterDeepLink)));
                    AppCenterService.TrackEvent("Launched Twitter Profile", "Method", "Twitter App");
                }
                catch (ActivityNotFoundException)
                {
                    OpenChromeCustomTabs(TwitterConstants.BrandonMinnickTwitterUrl);
                    AppCenterService.TrackEvent("Launched Twitter Profile", "Method", "Android Custom Tabs");
                }
            });
        }

        void OpenChromeCustomTabs(string url)
        {
            var tabsBuilder = new CustomTabsIntent.Builder();
            tabsBuilder.SetShowTitle(true);

            var intent = tabsBuilder.Build();
            intent.LaunchUrl(CurrentActivity, Android.Net.Uri.Parse(url));
        }
    }
}
