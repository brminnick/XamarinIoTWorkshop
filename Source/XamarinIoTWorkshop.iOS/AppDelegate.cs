using Foundation;
using UIKit;

using XamarinIoTWorkshop.Shared;

namespace XamarinIoTWorkshop.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            SyncfusionServices.InitializeSyncfusion();

            Microsoft.AppCenter.Distribute.Distribute.DontCheckForUpdatesInDebug();

            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.FormsMaps.Init(); 

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
