using System;
using BioinformaticsKKR.Core.Extensions;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Provider;
using FirstFloor.ModernUI.Presentation;

namespace BioinformaticsKKR.ViewModel
{
   

    public class SequencesLinksViewModel : ViewModelBase, BioinformaticsKKR.ViewModel.ISequencesLinksViewModel
    {
        #region Ctor
        public SequencesLinksViewModel()
        {
            SequencesRepository.Instance.Sequences.CollectionChanged += (s, e) => { InitializeCollection(); };
        }

        public void InitializeCollection()
        {
            SequencesLinkCollection = SequencesRepository.Instance.Sequences.CreateLinks("/View/CurrentSequence.xaml", UriKind.Relative);
            
        }

        #endregion

        #region Private Fields
        
        private LinkCollection _sequencesLinkCollection;

        #endregion



        public LinkCollection SequencesLinkCollection
        {
            get { return _sequencesLinkCollection; }
            set { _sequencesLinkCollection = value; OnPropertyChanged("SequencesLinkCollection"); }
        }
    }
}