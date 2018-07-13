using System;
using System.Numerics;
using System.Windows.Input;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public abstract class ThreeAxisViewModel : BaseViewModel
    {
        #region Fields
        double _xAxisValue, _yAxisValue, _zAxisValue;
        ICommand _sendDataCommand;
        #endregion

        #region Constructor
        protected ThreeAxisViewModel() => Device.StartTimer(TimeSpan.FromSeconds(1), SendData);
        #endregion

        #region Properties
        protected ICommand SendDataCommand => _sendDataCommand ??
            (_sendDataCommand = new Command(async () => await SendIoTData()));

        public double XAxisValue
        {
            get => _xAxisValue;
            set => SetProperty(ref _xAxisValue, value);
        }

        public double YAxisValue
        {
            get => _yAxisValue;
            set => SetProperty(ref _yAxisValue, value);
        }

        public double ZAxisValue
        {
            get => _zAxisValue;
            set => SetProperty(ref _zAxisValue, value);
        }
        #endregion

        #region Methods
        protected override void StopDataCollection()
        {
            base.StopDataCollection();

            XAxisValue = YAxisValue = ZAxisValue = 0;
        }

        protected void UpdateAxisValues(Vector3 vector)
        {
            XAxisValue = vector.X;
            YAxisValue = vector.Y;
            ZAxisValue = vector.Z;
        }

        protected bool SendData()
        {
            SendDataCommand?.Execute(null);

            return true;
        }

        protected abstract Task SendIoTData();
    }
    #endregion
}
