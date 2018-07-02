using System.Collections.ObjectModel;

using Syncfusion.SfGauge.XForms;

using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class CircularGaugeView : SfCircularGauge
    {
        public CircularGaugeView(string headerText, double startValue, double endValue)
        {
            Pointer = new NeedlePointer { AnimationDuration = 0.5 };

            var header = new Header
            {
                Text = headerText,
                ForegroundColor = Color.Gray
            };

            var circularGuageScale = new Scale
            {
                Interval = (endValue - startValue) / 10,
                StartValue = startValue,
                EndValue = endValue,
                ShowTicks = true,
                ShowLabels = true,
                Pointers = { Pointer },
                MinorTicksPerInterval = 4,
            };

            Scales = new ObservableCollection<Scale> { circularGuageScale };
            Headers = new ObservableCollection<Header> { header };
        }

        public NeedlePointer Pointer { get; }
    }
}
