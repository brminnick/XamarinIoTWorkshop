using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinIoTWorkshop
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            var tabbedPage = new Xamarin.Forms.TabbedPage
            {
                Children = {
                    new AccelerometerPage(),
                    new GeolocationPage(),
                    new GyroscopePage(),
                }
            };

            tabbedPage.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            MainPage = tabbedPage;
        }

        protected override void OnStart()
        {
            base.OnStart();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR LICENSE KEY");
        }
    }
}
