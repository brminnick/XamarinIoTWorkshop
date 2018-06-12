using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinIoTWorkshop
{
    public class App : Application
    {
        public App() => MainPage = new DataCollectionPage();
    }
}
