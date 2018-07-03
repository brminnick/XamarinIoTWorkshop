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

            var xCircularGuage = new CircularGaugeView("X-Axis", -1, 1);
            xCircularGuage.Pointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.XAxisValue));

            var yCircularGuage = new CircularGaugeView("Y-Axis", -1, 1);
            yCircularGuage.Pointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.YAxisValue));

            var zCircularGuage = new CircularGaugeView("Z-Axis", -5, 5);
            zCircularGuage.Pointer.SetBinding(Pointer.ValueProperty, nameof(ViewModel.ZAxisValue));

            var dataCollectionButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };
            dataCollectionButton.SetBinding(Button.CommandProperty, nameof(ViewModel.DataCollectionButtonCommand));
            dataCollectionButton.SetBinding(Button.TextProperty, nameof(ViewModel.DataCollectionButtonText));

            var grid = new Grid
            {
                Margin = new Thickness(0, 10),
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1,GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(25,GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                }
            };
            grid.Children.Add(xCircularGuage, 0, 0);
            grid.Children.Add(yCircularGuage, 0, 1);
            grid.Children.Add(zCircularGuage, 0, 2);
            grid.Children.Add(dataCollectionButton, 0, 3);

            Content = grid;
        }
    }
}
