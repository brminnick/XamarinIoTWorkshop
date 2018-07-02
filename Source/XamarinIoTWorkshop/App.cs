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
                    new SettingsPage()
                }
            };

            tabbedPage.On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);

            MainPage = tabbedPage;
        }
    }
}
