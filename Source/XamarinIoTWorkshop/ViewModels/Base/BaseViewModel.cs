using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinIoTWorkshop
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Constant Fields
        protected const string _beginDataCollectionText = "Begin Data Collection";
        protected const string _endDataCollectionText = "End Data Collection";
        #endregion

        #region Fields
        bool _isDataCollectionActive;
        string _dataCollectionButtonText = _endDataCollectionText;
        ICommand _dataCollectionButtonCommand;
        #endregion

        #region Constructors
        public BaseViewModel() => StartDataCollection();
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<bool> DataCollectionStatusChanged;
        public event EventHandler<Type> FeatureNotSupportedExceptionThrown;
        #endregion

        #region Properties
        public ICommand DataCollectionButtonCommand => _dataCollectionButtonCommand ??
            (_dataCollectionButtonCommand = new Command(ExecuteDataCollectionButtonCommand));

        public string DataCollectionButtonText
        {
            get => _dataCollectionButtonText;
            set => SetProperty(ref _dataCollectionButtonText, value);
        }

        protected bool IsDataCollectionActive
        {
            get => _isDataCollectionActive;
            private set => SetProperty(ref _isDataCollectionActive, value, () => OnDataCollectionStatusChanged(value));
        }
        #endregion

        #region Methods
        protected virtual void StartDataCollection()
        {
            IsDataCollectionActive = true;
        }

        protected virtual void StopDataCollection()
        {
            IsDataCollectionActive = false;
        }

        protected void SetProperty<T>(ref T backingStore, T value, Action onChanged = null, [CallerMemberName] string propertyname = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            onChanged?.Invoke();

            OnPropertyChanged(propertyname);
        }

        protected void OnFeatureNotSupportedExceptionThrown(Type xamarinEssentialsType) =>
            FeatureNotSupportedExceptionThrown?.Invoke(this, xamarinEssentialsType);

        void ExecuteDataCollectionButtonCommand()
        {
            if (DataCollectionButtonText.Equals(_beginDataCollectionText))
            {
                StartDataCollection();
                DataCollectionButtonText = _endDataCollectionText;
            }
            else
            {
                StopDataCollection();
                DataCollectionButtonText = _beginDataCollectionText;
            }
        }

        void OnPropertyChanged([CallerMemberName]string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        void OnDataCollectionStatusChanged(bool isCollectingData) => DataCollectionStatusChanged?.Invoke(this, isCollectingData);
        #endregion
    }
}
