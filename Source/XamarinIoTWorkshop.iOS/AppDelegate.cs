using Foundation;
using UIKit;

namespace XamarinIoTWorkshop.iOS
{
    [Register(nameof(AppDelegate))]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication uiApplication, NSDictionary launchOptions)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionConstants.LicenseKey);

            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(uiApplication, launchOptions);
        }
    }
}
