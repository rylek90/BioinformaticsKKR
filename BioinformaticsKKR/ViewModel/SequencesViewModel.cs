using System;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Core.Extensions;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.IO;
using BioinformaticsKKR.Provider;
using FirstFloor.ModernUI.Presentation;
using CommandBase = BioinformaticsKKR.Core.ViewModel.CommandBase;

namespace BioinformaticsKKR.ViewModel
{
    public class SequencesViewModel : ViewModelBase
    {
        #region Ctor
        public SequencesViewModel()
        {
            InitializeCollection();
        }

        private void InitializeCollection()
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