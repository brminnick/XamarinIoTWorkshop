using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace XamarinIoTWorkshop
{
    public class DataCollectionViewModel : BaseViewModel
    {
        #region Constant Fields
        const string _beginDataCollectionText = "Begin Data Collection";
        const string _endDataCollectionText = "End Data Collection";
        #endregion

        #region Fields
        string _dataCollectedLabelText;
        string _dataCollectionButtonText = _beginDataCollectionText;
        ICommand _dataCollectionButtonCommand;
        #endregion

        #region Properties
        public ICommand DataCollectionButtonCommand => _dataCollectionButtonCommand ??
            (_dataCollectionButtonCommand = new Command(async () => await ExecuteDataCollectionButtonCommand()));

        public string DataCollectionButtonText
        {
            get => _dataCollectionButtonText;
            set => SetProperty(ref _dataCollectionButtonText, value);
        }

        public string DataCollectedLabelText
        {
            get => _dataCollectedLabelText;
            set => SetProperty(ref _dataCollectedLabelText, value);
        }
        #endregion

        #region Methods
        Task ExecuteDataCollectionButtonCommand()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
