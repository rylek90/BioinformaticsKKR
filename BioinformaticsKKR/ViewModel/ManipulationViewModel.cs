using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Assembly;

namespace BioinformaticsKKR.ViewModel
{
    public interface IManipulationViewModel
    {
        string Status { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class ManipulationViewModel : ViewModelBase, IManipulationViewModel
    {
        private readonly IStatusViewModel _statusService;

        public string Status
        {
            get { return _statusService.LastStatus; }
            set
            {
                _statusService.LastStatus = value;
                OnPropertyChanged("Status");
            }
        }

        public ManipulationViewModel(IStatusViewModel statusService
            )
        {
            _statusService = statusService;
            _statusService.PropertyChanged += (sender, e) => { OnPropertyChanged(e.ToString()); };
           
        }

    }
}