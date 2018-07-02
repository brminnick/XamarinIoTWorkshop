using System;

using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class SettingsPage : BaseContentPage<SettingsViewModel>
    {
        #region Constructors
        public SettingsPage()
        {
            Icon = "Settings";
            Title = "Settings";

            var dataCollectionButton = new Button();
            dataCollectionButton.SetBinding(Button.TextProperty, nameof(ViewModel.DataCollectionButtonText));
            dataCollectionButton.SetBinding(Button.CommandProperty, nameof(ViewModel.DataCollectionButtonCommand));

            var dataCollectedLabel = new Label();
            dataCollectedLabel.SetBinding(Label.TextProperty, nameof(ViewModel.DataCollectedLabelText));

            var flexLayout = new FlexLayout
            {
                Direction = FlexDirection.Column,
                AlignItems = FlexAlignItems.Center,
                Children = {
                    dataCollectionButton,
                    dataCollectedLabel
                }
            };

            Padding = new Thickness(20);

            Content = flexLayout;
        }
        #endregion

        #region Methods
        protected override void SubscribeEventHandlers()
        {
            ViewModel.FeatureNotSupportedExceptionThrown += HandleFeatureNotSupportedExceptionThrown;
        }

        protected override void UnsubscribeEventHandlers()
        {
            ViewModel.FeatureNotSupportedExceptionThrown -= HandleFeatureNotSupportedExceptionThrown;
        }

        void HandleFeatureNotSupportedExceptionThrown(object sender, Type type) =>
            Device.BeginInvokeOnMainThread(async () => await DisplayAlert($"{type.Name} Unavailable", $"{type.Name} data will not be collected because it is not supported by this device", "OK"));
        #endregion
    }
}
