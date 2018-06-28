using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinIoTWorkshop
{
    public class App : Xamarin.Forms.Application
    {
        public App()
        {
            var tabbedPage = new Xamarin.Forms.TabbedPage();

            tabbedPage.Children.Add(new AccelerometerPage
            {
                Icon = "Accelerometer",
                Title = "Accelerometer"
            });

            tabbedPage.Children.Add(new GeolocationPage
            {
                Icon = "Geolocation",
                Title = "Geolocation"
            });

            tabbedPage.Children.Add(new GyroscopePage
            {
                Icon = "Gyroscope",
                Title = "Gyroscope"
            });

            tabbedPage.Children.Add(new SettingsPage
            {
                Icon = "Settings",
                Title = "Settings"
            });

            tabbedPage.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            MainPage = tabbedPage;
        }
    }
}
