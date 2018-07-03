using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace XamarinIoTWorkshop
{
    public abstract class BaseContentPage<T> : ContentPage where T : BaseViewModel, new()
    {
        #region Constructors
        protected BaseContentPage()
        {
            BindingContext = ViewModel;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }
        #endregion

        #region Properties
        protected T ViewModel { get; } = new T();
        #endregion

        #region Methods
        protected virtual void SubscribeEventHandlers()
        {

        }

        protected virtual void UnsubscribeEventHandlers()
        {

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            SubscribeEventHandlers();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            UnsubscribeEventHandlers();
        }
        #endregion
    }
}
