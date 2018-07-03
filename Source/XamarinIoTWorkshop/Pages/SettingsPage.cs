using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class SettingsPage : ContentPage
    {
        readonly Entry _iotHubConnectionStringValueEntry;

        public SettingsPage()
        {
            Icon = "Settings";
            Title = "Settings";

            var iotHubConnectionStringTitleLabel = new Label
            {
                Text = "IoT Hub Connection String",
                FontAttributes = FontAttributes.Bold
            };

            _iotHubConnectionStringValueEntry = new Entry { Placeholder = "IoT Hub Connection String" };

            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    iotHubConnectionStringTitleLabel,
                    _iotHubConnectionStringValueEntry
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _iotHubConnectionStringValueEntry.Text = IotHubSettings.IotHubConnectionString;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IotHubSettings.IotHubConnectionString = _iotHubConnectionStringValueEntry.Text;
        }
    }
}
