using System.ComponentModel;

namespace BioinformaticsKKR.Core.ViewModel
{
    public interface IViewModelBase
    {
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class ViewModelBase : INotifyPropertyChanged, IViewModelBase
    {
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}