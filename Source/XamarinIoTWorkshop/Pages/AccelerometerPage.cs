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

            var circularGuageScalePointer = new NeedlePointer();
            circularGuageScalePointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.XValue));

            var circularGuageScale = new Scale
            {
                Interval = 0.2,
                StartValue = -1.0,
                EndValue = 1.0,
                ShowTicks = true,
                ShowLabels = true,
                Pointers = { circularGuageScalePointer },
                MinorTicksPerInterval = 4
            };

            var circularGuage = new SfCircularGauge
            {
                Scales = { circularGuageScale }
            };

            var dataCollectionButton = new Button();
            dataCollectionButton.SetBinding(Button.CommandProperty, nameof(ViewModel.DataCollectionButtonCommand));
            dataCollectionButton.SetBinding(Button.TextProperty, nameof(ViewModel.DataCollectionButtonText));

            var flexLayout = new FlexLayout
            {
                Margin = new Thickness(30),
                Direction = FlexDirection.Column,
                AlignItems = FlexAlignItems.Center,
                AlignContent = FlexAlignContent.End,
                Children = {
                    circularGuage,
                    dataCollectionButton,
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
