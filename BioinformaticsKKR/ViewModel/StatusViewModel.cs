using System;
using System.Collections.Generic;
using System.Linq;
using Bio.SimilarityMatrices;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Alignement;
using System.ComponentModel;

namespace BioinformaticsKKR.ViewModel
{
    public interface IStatusViewModel
    {
        string LastStatus { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class StatusViewModel : ViewModelBase, IStatusViewModel
    {
        public StatusViewModel()
        {
            LastStatus = "Status";
        }

        private string _lastStatus;

        public string LastStatus
        {
            get { return _lastStatus; }
            set
            {
                _lastStatus = value;
                OnPropertyChanged("LastStatus");
            }
        }
    }
}