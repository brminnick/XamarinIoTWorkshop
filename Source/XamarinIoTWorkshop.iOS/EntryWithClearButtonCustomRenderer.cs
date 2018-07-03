using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

using XamarinIoTWorkshop.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(EntryWithClearButtonCustomRenderer))]
namespace XamarinIoTWorkshop.iOS
{
	public class EntryWithClearButtonCustomRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control is UITextField uiTextField)
                uiTextField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
        }
    }
}
