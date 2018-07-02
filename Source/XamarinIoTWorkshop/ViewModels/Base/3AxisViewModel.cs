using System.Threading.Tasks;
using System.Numerics;

namespace XamarinIoTWorkshop
{
    public class ThreeAxisViewModel : BaseViewModel
    {
        #region Fields
        double _xAxisValue, _yAxisValue, _zAxisValue;
        #endregion

        #region Properties
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
        #endregion
    }
}
