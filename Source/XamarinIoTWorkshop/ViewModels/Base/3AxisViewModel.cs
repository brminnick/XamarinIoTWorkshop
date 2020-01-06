using System;
using System.Numerics;
using System.Threading.Tasks;
using AsyncAwaitBestPractices;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    abstract class ThreeAxisViewModel : BaseViewModel
    {
        double _xAxisValue, _yAxisValue, _zAxisValue;

        protected ThreeAxisViewModel() => Device.StartTimer(TimeSpan.FromSeconds(1), SendData);

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
            SendIoTData().SafeFireAndForget(ex => Console.WriteLine(ex));
            return true;
        }

        protected abstract Task SendIoTData();
    }
}
