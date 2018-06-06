using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class DataCollectionPage : BaseContentPage<DataCollectionViewModel>
    {
        #region Constant Fields
        readonly Button _dataCollectionButton;
        #endregion

        #region Constructors
        public DataCollectionPage()
        {
            _dataCollectionButton = new Button();
            _dataCollectionButton.SetBinding(Button.TextProperty, nameof(ViewModel.DataCollectionButtonText));

            var dataCollectedLabel = new Label();
            dataCollectedLabel.SetBinding(Label.TextProperty, nameof(ViewModel.DataCollectedLabelText));

            var flexLayout = new FlexLayout
            {
                Children = {
                    _dataCollectionButton,
                    dataCollectedLabel
                }
            };

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
