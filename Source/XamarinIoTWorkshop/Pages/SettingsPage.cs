using System;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class SettingsPage : ContentPage
    {
        readonly Switch _isSendDataToAzureEnabledSwitch;
        readonly Editor _deviceConnectionStringEditor;

        public SettingsPage()
        {
            const int labelHeight = 20;

            Padding = new Thickness(20);
            Title = "Settings";
            BackgroundColor = Color.FromHex("F4F3FA");

            var deviceConnectionStringLabel = new Label
            {
                Text = "Device Connection String",
                FontAttributes = FontAttributes.Bold
            };

            var isSendDataToAzureEnabledLabel = new Label
            {
                Text = "Send Data To Azure",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start,
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };

            _isSendDataToAzureEnabledSwitch = new Switch
            {
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            _isSendDataToAzureEnabledSwitch.Toggled += HandleIsSendDataToAzureEnabledSwitchToggled;

            _deviceConnectionStringEditor = new Editor
            {
                IsSpellCheckEnabled = false,
            };
            _deviceConnectionStringEditor.Completed += HandleDeviceConnectionStringEditorCompleted;

            var createdByLabel = new Label
            {
                Text = "App Created by Brandon Minnick",
                FontSize = 12,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.End
            };
            createdByLabel.GestureRecognizers.Add(new TapGestureRecognizer { Command = new AsyncCommand(CreatedByLabelTapped) });

            var grid = new Grid
            {
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(labelHeight, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(labelHeight, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(labelHeight, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };

            grid.Children.Add(deviceConnectionStringLabel, 0, 0);
            grid.Children.Add(_deviceConnectionStringEditor, 0, 1);
            grid.Children.Add(isSendDataToAzureEnabledLabel, 0, 3);
            grid.Children.Add(_isSendDataToAzureEnabledSwitch, 0, 4);
            grid.Children.Add(createdByLabel, 0, 4);

            Content = grid;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            _deviceConnectionStringEditor.Text = IotHubSettings.DeviceConnectionString;
            _isSendDataToAzureEnabledSwitch.IsToggled = IotHubSettings.IsSendDataToAzureEnabled;
        }

        Task CreatedByLabelTapped() => DeepLinkingService.OpenApp(TwitterConstants.BrandonMinnickTwitterDeepLink, TwitterConstants.BrandonMinnickTwitterUrl);

        void HandleIsSendDataToAzureEnabledSwitchToggled(object sender, ToggledEventArgs e) =>
            IotHubSettings.IsSendDataToAzureEnabled = _isSendDataToAzureEnabledSwitch.IsToggled;

        void HandleDeviceConnectionStringEditorCompleted(object sender, EventArgs e) =>
            IotHubSettings.DeviceConnectionString = _deviceConnectionStringEditor.Text;
    }
}
