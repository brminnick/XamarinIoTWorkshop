using Syncfusion.SfGauge.XForms;

using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class GyroscopePage : BaseContentPage<GyroscopeViewModel>
    {
        public GyroscopePage()
        {
            Icon = "Gyroscope";
            Title = "Gyroscope";

            var xCircularGauge = new CircularGaugeView("X-Axis", -1, 1);
            xCircularGauge.Pointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.XAxisValue));

            var yCircularGauge = new CircularGaugeView("Y-Axis", -1, 1);
            yCircularGauge.Pointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.YAxisValue));

            var zCircularGauge = new CircularGaugeView("Z-Axis", -5, 5);
            zCircularGauge.Pointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.ZAxisValue));

            var dataCollectionButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            dataCollectionButton.SetBinding(Button.CommandProperty, nameof(ViewModel.DataCollectionButtonCommand));
            dataCollectionButton.SetBinding(Button.TextProperty, nameof(ViewModel.DataCollectionButtonText));

            var grid = new Grid
            {
                Margin = new Thickness(0, 20),
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(0.25, GridUnitType.Star) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(xCircularGauge, 0, 0);
            grid.Children.Add(yCircularGauge, 0, 1);
            grid.Children.Add(zCircularGauge, 0, 2);
            grid.Children.Add(dataCollectionButton, 0, 3);

            Content = grid;
        }
    }
}
