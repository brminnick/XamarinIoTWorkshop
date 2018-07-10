using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinIoTWorkshop
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            var accelerometerPage = new AccelerometerPage();
            var gyroscopePage = new GyroscopePage();
            var settingsNavigationPage = new Xamarin.Forms.NavigationPage(new SettingsPage())
            {
                Icon = "Settings",
                Title = "Settings",
                BarBackgroundColor = Xamarin.Forms.Color.White,
                BarTextColor = Xamarin.Forms.Color.Black
            };

            settingsNavigationPage.On<iOS>().SetPrefersLargeTitles(true);

            var tabbedPage = new Xamarin.Forms.TabbedPage
            {
                Children = {
                    accelerometerPage,
                    gyroscopePage,
                    settingsNavigationPage
                }
            };

            tabbedPage.On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            MainPage = tabbedPage;
        }

        protected override void OnStart()
        {
            base.OnStart();

            AppCenterService.Start();
        }
    }
}
