using Foundation;
using UIKit;

using XamarinIoTWorkshop.SyncFusion.Shared;

namespace XamarinIoTWorkshop.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            SyncfusionServices.InitializeSyncfusion();

            global::Xamarin.Forms.Forms.Init();
            global::Xamarin.FormsMaps.Init();

#if DEBUG
            Xamarin.Calabash.Start();
#endif

            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
