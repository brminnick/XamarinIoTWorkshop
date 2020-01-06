using System.Collections.ObjectModel;
using Syncfusion.SfGauge.XForms;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class CircularGaugeView : SfCircularGauge
    {
        public CircularGaugeView(in string headerText, in double startValue, in double endValue)
        {
            Pointer = new NeedlePointer { AnimationDuration = 0.5 };

            var header = new Header
            {
                Text = headerText,
                ForegroundColor = Color.Gray
            };

            var circularGaugeScale = new Scale
            {
                Interval = (endValue - startValue) / 10,
                StartValue = startValue,
                EndValue = endValue,
                ShowTicks = true,
                ShowLabels = true,
                Pointers = { Pointer },
                MinorTicksPerInterval = 4,
            };

            Scales = new ObservableCollection<Scale> { circularGaugeScale };
            Headers = new ObservableCollection<Header> { header };
        }

        public NeedlePointer Pointer { get; }
    }
}
