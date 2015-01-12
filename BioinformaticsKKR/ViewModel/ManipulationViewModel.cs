using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Assembly;
using Bio;
using BioinformaticsKKR.Core.Definitions.SimilarityMatrices;
using BioinformaticsKKR.Service.Alignement;
using Bio.SimilarityMatrices;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows;
using BioinformaticsKKR.Service.Modification;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.ViewModel
{
    public interface IManipulationViewModel
    {
        string Status { get; set; }
        IEnumerable<IModificatorSequences> SequencesModificators { get; }
        IModificatorSequences CurrentModificator { get; set; }
        ISequence SequenceSelected { get; set; }
        ISequence ModificatedSequence { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
        IEnumerable<ISequence> SequencesList { get; set; }
        CommandBase ManipulateCommand { get; set; }
    }

    public class ManipulationViewModel : ViewModelBase, IManipulationViewModel
    {
        private readonly IEnumerable<IModificatorSequences> _sequencesModificators;
        private IModificatorSequences _currentModificator;
        private readonly IStatusViewModel _statusService;
        private readonly IManipulationSequenceViewModel _manipulationSequenceViewModel;
        private IEnumerable<ISequence> _sequencesList;
        private ISequence _sequenceSelected;
        private ISequence _modified;
        public CommandBase ManipulateCommand { get; set; }
        public CommandBase SaveCommand { get; set; }

        public string Status
        {
            get { return _statusService.LastStatus; }
            set
            {
                _statusService.LastStatus = value;
                OnPropertyChanged("Status");
            }
        }

        public IManipulationSequenceViewModel ManipulationSequenceViewModel
        {
            get { return _manipulationSequenceViewModel; }
        }

        public ManipulationViewModel(IEnumerable<IModificatorSequences> sequencesModificators,
            IStatusViewModel statusService,
            IManipulationSequenceViewModel manipulationSequenceViewModel,
            IEnumerable<IAmSimilarityMatrix> similarityMatrices
            )
        {
            _statusService = statusService;
            _manipulationSequenceViewModel = manipulationSequenceViewModel;
            _statusService.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());
            _sequencesModificators = sequencesModificators;
            _manipulationSequenceViewModel.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());

            ManipulateCommand = new CommandBase
            {
                CanExecuteMethod = CanManipulate,
                ExecuteMethod = ExecuteManipulate
            };

            SaveCommand = new CommandBase
            {
                CanExecuteMethod = CanSave,
                ExecuteMethod = ExecuteSave
            };
        }

        private bool CanSave(object obj)
        {
            if (ModificatedSequence == null)
                return false;
            return true;
        }

        private void ExecuteSave(object obj)
        {
            if (ModificatedSequence == null) return;
            ModificatedSequence.ID += "__modified";
            SequencesRepository.Instance.Sequences.Add(ModificatedSequence);
            //save
        }

        private void ExecuteManipulate(object obj)
        {
            try
            {
                //LOGIKA MANIPULACJI
                var sequence = _currentModificator.Modify(SequenceSelected);
                ModificatedSequence = sequence;
                SaveCommand.UpdateCanExecuteState();
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Warning!", MessageBoxButton.OK);
            }
        }


        private bool CanManipulate(object obj)
        {
            if (_currentModificator == null
                || _sequenceSelected == null)
                return false;

            /*  if (_currentAligner.CanAlignSequences(_firstSequenceSelected, _secondSequenceSelected))
                return true;*/

            return true;
        }

        public IEnumerable<IModificatorSequences> SequencesModificators
        {
            get { return _sequencesModificators; }
        }

        public IModificatorSequences CurrentModificator
        {
            get { return _currentModificator; }
            set
            {
                _currentModificator = value;
                ManipulateCommand.UpdateCanExecuteState();
                OnPropertyChanged("CurrentModificator");
            }
        }

        public IEnumerable<ISequence> SequencesList
        {
            get { return _sequencesList; }
            set
            {
                _sequencesList = value;
                ManipulateCommand.UpdateCanExecuteState();
                OnPropertyChanged("SequenceList");
            }
        }

        public ISequence SequenceSelected
        {
            get { return _sequenceSelected; }
            set
            {
                _sequenceSelected = value;
                ManipulateCommand.UpdateCanExecuteState();
                _manipulationSequenceViewModel.SequenceA = value;
                UpdateModificatorsState();
                OnPropertyChanged("FirstSequenceSelected");
            }
        }

        private void UpdateModificatorsState()
        {
            // SequencesAligners =
            //         _sequencesAligners.Where(x => x.CanAlignSequences(_firstSequenceSelected, _secondSequenceSelected)).ToList();
        }


        public ISequence ModificatedSequence
        {
            get { return _modified; }
            set
            {
                //set
                _modified = value;
                OnPropertyChanged("ModificatedSequence");
            }
        }
    }
}