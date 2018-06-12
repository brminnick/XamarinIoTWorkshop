using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class DataCollectionPage : BaseContentPage<DataCollectionViewModel>
    {
        #region Constructors
        public DataCollectionPage()
        {
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

        }

        protected override void UnsubscribeEventHandlers()
        {

        }
        #endregion
    }
}
