using Android.App;
using Android.OS;

namespace XamarinIoTWorkshop.Droid
{
    [Activity(Label = "XamarinIoTWorkshop", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionConstants.LicenseKey);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);

            LoadApplication(new App());
        }
    }
}

