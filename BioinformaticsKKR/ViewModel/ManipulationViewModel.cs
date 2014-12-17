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

namespace BioinformaticsKKR.ViewModel
{
    public interface IManipulationViewModel
    {
        string Status { get; set; }
        IEnumerable<IAmSimilarityMatrix> SimilarityMatrices { get; }
        IAmSimilarityMatrix CurrentSimilarityMatrix { get; set; }
        List<IAlignSequences> SequencesAligners { get; }
        IAlignSequences CurrentAligner { get; set; }
        ISequence FirstSequenceSelected { get; set; }
        ISequence SecondSequenceSelected { get; set; }
        ISequence Aligned { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;

        IEnumerable<ISequence> FirstSequencesList { get; set; }
        IEnumerable<ISequence> SecondSequencesList { get; set; }
    }

    public class ManipulationViewModel : ViewModelBase, IManipulationViewModel
    {
        private readonly IEnumerable<IAlignSequences> _sequencesAligners;
        private IAlignSequences _currentAligner;
        private IAmSimilarityMatrix _currentSimilarityMatrix;
        private readonly IStatusViewModel _statusService;
        private readonly IManipulationSequenceViewModel _alignmentSequenceViewModel;
        private IEnumerable<ISequence> _secondSequencesList;
        private IEnumerable<ISequence> _firstSequencesList;
        private ISequence _secondSequenceSelected;
        private ISequence _firstSequenceSelected;
        private int _gapPenalty;
        private ISequence _aligned;
        private List<IAlignSequences> _availableSequenceAligners;
        private IEnumerable<IAmSimilarityMatrix> _availableSimilarityMatrices;
        private readonly IEnumerable<IAmSimilarityMatrix> _similarityMatrices;

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
            get { return _alignmentSequenceViewModel; }
        }

        public ISequence Aligned
        {
            get { return _aligned; }
            set
            {
                _aligned = value;
                OnPropertyChanged("Aligned");
            }
        }

        public ManipulationViewModel(IEnumerable<IAlignSequences> sequencesAligners,
            IStatusViewModel statusService,
            IManipulationSequenceViewModel alignmentSequenceViewModel,
            IEnumerable<IAmSimilarityMatrix> similarityMatrices
            )
        {
            _statusService = statusService;
            _alignmentSequenceViewModel = alignmentSequenceViewModel;
            _statusService.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());
            _sequencesAligners = sequencesAligners;
            _similarityMatrices = similarityMatrices;
            _alignmentSequenceViewModel.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());

            AlignCommand = new CommandBase
            {
                CanExecuteMethod = CanAlign,
                ExecuteMethod = ExecuteAlign
            };
        }

        public CommandBase AlignCommand { get; set; }

        private void ExecuteAlign(object obj)
        {
            try
                {
                    _currentAligner.GapPenalty = GapPenalty;
                    _currentAligner.SimilarityMatrix = new SimilarityMatrix(_currentSimilarityMatrix.Matrix);
                    var sequence = _currentAligner.Align(FirstSequenceSelected, SecondSequenceSelected);

                    // WTF!!!!!!
                    Aligned = sequence.First().PairwiseAlignedSequences.First().Consensus;
                }
                catch (Exception ex)
                {
                    ModernDialog.ShowMessage(ex.Message, "Warning!", MessageBoxButton.OK);
                }
        }

        public int GapPenalty
        {
            get { return _gapPenalty; }
            set
            {
                _gapPenalty = value;
                OnPropertyChanged("GapPenalty");
            }
        }

        private bool CanAlign(object obj)
        {
            if (_currentAligner == null
                || _firstSequenceSelected == null
                || _secondSequenceSelected == null)
                return false;

            if (_currentAligner.CanAlignSequences(_firstSequenceSelected, _secondSequenceSelected))
                return true;

            return true;
        }

        public IEnumerable<IAmSimilarityMatrix> SimilarityMatrices
        {
            get { return _availableSimilarityMatrices; }
            set
            {
                _availableSimilarityMatrices = value;
                OnPropertyChanged("SimilarityMatrices");
            }
        }

        public IAmSimilarityMatrix CurrentSimilarityMatrix
        {
            get { return _currentSimilarityMatrix; }
            set
            {
                _currentSimilarityMatrix = value;
                AlignCommand.UpdateCanExecuteState();
                OnPropertyChanged("CurrentSimilarityMatrix");
            }
        }

        

        public List<IAlignSequences> SequencesAligners
        {
            get { return _availableSequenceAligners; }
            set
            {
                _availableSequenceAligners = value;
                OnPropertyChanged("SequencesAligners");
            }
        }

        public IAlignSequences CurrentAligner
        {
            get { return _currentAligner; }
            set { _currentAligner = value; AlignCommand.UpdateCanExecuteState(); OnPropertyChanged("CurrentAligner"); }
        }

        public IEnumerable<ISequence> FirstSequencesList
        {
            get { return _firstSequencesList; }
            set
            {
                _firstSequencesList = value;
                AlignCommand.UpdateCanExecuteState();
                OnPropertyChanged("FirstSequenceList");
            }
        }

        public ISequence FirstSequenceSelected
        {
            get { return _firstSequenceSelected; }
            set
            {
                _firstSequenceSelected = value;
                AlignCommand.UpdateCanExecuteState();
                _alignmentSequenceViewModel.SequenceA = value;
                UpdateAlignersState();
                UpdateMatricesState();
                OnPropertyChanged("FirstSequenceSelected");
            }
        }

        private void UpdateAlignersState()
        {
            SequencesAligners =
                    _sequencesAligners.Where(x => x.CanAlignSequences(_firstSequenceSelected, _secondSequenceSelected)).ToList();
        }

        private void UpdateMatricesState()
        {
            SimilarityMatrices =
                _similarityMatrices.Where(x => x.CanAlign(FirstSequenceSelected, SecondSequenceSelected));
        }

        public IEnumerable<ISequence> SecondSequencesList
        {
            get { return _secondSequencesList; }
            set
            {
                _secondSequencesList = value;
                AlignCommand.UpdateCanExecuteState();
                OnPropertyChanged("SecondSequenceList");
            }
        }

        public ISequence SecondSequenceSelected
        {
            get { return _secondSequenceSelected; }
            set
            {
                _secondSequenceSelected = value;
                AlignCommand.UpdateCanExecuteState();
                UpdateAlignersState();
                UpdateMatricesState();
                OnPropertyChanged("SecondSequenceSelected");
            }
        }
    }
}