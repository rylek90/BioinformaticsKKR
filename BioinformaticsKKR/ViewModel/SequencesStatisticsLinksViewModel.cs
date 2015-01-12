using System;
using System.ComponentModel;
using BioinformaticsKKR.Core.Extensions;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Provider;
using FirstFloor.ModernUI.Presentation;

namespace BioinformaticsKKR.ViewModel
{
    public interface ISequencesStatisticsLinksViewModel
    {
        void InitializeCollection();
        LinkCollection SequencesStatisticsLinkCollection { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class SequencesStatisticsLinksViewModel : ViewModelBase, ISequencesStatisticsLinksViewModel
    {
        #region Ctor

        public SequencesStatisticsLinksViewModel()
        {
            SequencesRepository.Instance.Sequences.CollectionChanged += (s, e) => InitializeCollection();
        }

        public void InitializeCollection()
        {
            SequencesStatisticsLinkCollection =
                SequencesRepository.Instance.Sequences.CreateLinks("/View/CurrentSequenceStatistics.xaml",
                    UriKind.Relative);
        }

        #endregion

        #region Private Fields

        private LinkCollection _sequencesLinkCollection;

        #endregion

        public LinkCollection SequencesStatisticsLinkCollection
        {
            get { return _sequencesLinkCollection; }
            set
            {
                _sequencesLinkCollection = value;
                OnPropertyChanged("SequencesStatisticsLinkCollection");
            }
        }
    }
}