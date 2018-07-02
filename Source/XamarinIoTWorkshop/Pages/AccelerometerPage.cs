using Xamarin.Forms;
using Syncfusion.SfGauge.XForms;

namespace XamarinIoTWorkshop
{
    public class AccelerometerPage : BaseContentPage<AccelerometerViewModel>
    {
        public AccelerometerPage()
        {
            Icon = "Accelerometer";
            Title = "Accelerometer";

            var circularGuage = new SfCircularGauge();
            var circularGuageScale = new Scale
            {
                StartValue = -1,
                EndValue = 1,
            };
            var circularGuageScalePointer = new Pointer();
            circularGuageScalePointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.XValue));

            circularGuage.Scales.Add(circularGuageScale);
            circularGuageScale.Pointers.Add(circularGuageScalePointer);

            var dataCollectionButton = new Button();
            dataCollectionButton.SetBinding(Button.CommandProperty, nameof(ViewModel.DataCollectionButtonCommand));
            dataCollectionButton.SetBinding(Button.TextProperty, nameof(ViewModel.DataCollectionButtonText));

            var flexLayout = new FlexLayout
            {
                Margin = new Thickness(30),
                Direction = FlexDirection.Column,
                AlignItems = FlexAlignItems.Center,
                Children = {
                    dataCollectionButton,
                    circularGuage
                }
            };

            Content = flexLayout;
        }

        protected override void SubscribeEventHandlers()
        {

        }

        protected override void UnsubscribeEventHandlers()
        {

        }
    }
}
