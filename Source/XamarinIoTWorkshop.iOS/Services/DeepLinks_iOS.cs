using System.Threading.Tasks;

using CoreFoundation;
using Foundation;
using SafariServices;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(XamarinIoTWorkshop.iOS.DeepLinks_iOS))]
namespace XamarinIoTWorkshop.iOS
{
    public class DeepLinks_iOS : IDeepLinks
    {
        readonly NSUrl _twitterWebUrl = new NSUrl(TwitterConstants.BrandonMinnickTwitterUrl);
        readonly NSUrl _twitterAppUrl = new NSUrl(TwitterConstants.BrandonMinnickTwitterDeepLink);

        public async Task OpenTwitter()
        {
            var tcs = new TaskCompletionSource<bool>();

            DispatchQueue.MainQueue.DispatchAsync(() => tcs.SetResult(UIApplication.SharedApplication.CanOpenUrl(_twitterAppUrl)));

            var canOpenTwitterApp = await tcs.Task.ConfigureAwait(false);

            if (canOpenTwitterApp)
                OpenLinkInApp(_twitterAppUrl);
            else
                await OpenLinkInSFSafariViewController(_twitterWebUrl).ConfigureAwait(false);
        }

        async Task OpenLinkInSFSafariViewController(NSUrl url)
        {
            var safariViewController = new SFSafariViewController(url);

            var visibleViewController = await GetVisibleViewController().ConfigureAwait(false);

            DispatchQueue.MainQueue.DispatchAsync(() => visibleViewController.PresentViewControllerAsync(safariViewController, true));

            AppCenterService.TrackEvent("Launched Twitter Profile", "Method", "SFSafariViewController");
        }

        void OpenLinkInApp(NSUrl url)
        {
            DispatchQueue.MainQueue.DispatchAsync(() => UIApplication.SharedApplication.OpenUrl(url, new UIApplicationOpenUrlOptions { SourceApplication = "com.minnick.XamarinIoTWorkshop" }, null));

            AppCenterService.TrackEvent("Launched Twitter Profile", "Method", "Twitter App");
        }

        static Task<UIViewController> GetVisibleViewController()
        {
            var tcs = new TaskCompletionSource<UIViewController>();

            DispatchQueue.MainQueue.DispatchAsync(() =>
            {
                var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

                switch (rootController.PresentedViewController)
                {
                    case UINavigationController navigationController:
                        tcs.SetResult(navigationController.TopViewController);
                        break;

                    case UITabBarController tabBarController:
                        tcs.SetResult(tabBarController.SelectedViewController);
                        break;

                    case null:
                        tcs.SetResult(rootController);
                        break;

                    default:
                        tcs.SetResult(rootController.PresentedViewController);
                        break;
                }
            });

            return tcs.Task;
        }
    }
}
