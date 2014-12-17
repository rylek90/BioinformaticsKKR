using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Bio;
using Bio.SimilarityMatrices;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Alignement;

namespace BioinformaticsKKR.ViewModel
{
    public interface IAlignViewModel
    {
        string Status { get; set; }
        Array SimilarityMatrices { get; }
        SimilarityMatrix.StandardSimilarityMatrix CurrentSimilarityMatrix { get; set; }
        List<IAlignSequences> SequencesAligners { get; }
        IAlignSequences CurrentAligner { get; set; }
        ISequence FirstSequenceSelected { get; set; }
        ISequence SecondSequenceSelected { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;

        IEnumerable<ISequence> FirstSequencesList { get; set; }
        IEnumerable<ISequence> SecondSequencesList { get; set; }
    }

    public class AlignViewModel : ViewModelBase, IAlignViewModel
    {
        private readonly IEnumerable<IAlignSequences> _sequencesAligners;
        private IAlignSequences _currentAligner;
        private SimilarityMatrix.StandardSimilarityMatrix _currentSimilarityMatrix;
        private readonly IStatusViewModel _statusService;
        private readonly IAlignmentSequenceViewModel _alignmentSequenceViewModel;
        private IEnumerable<ISequence> _secondSequencesList;
        private IEnumerable<ISequence> _firstSequencesList;
        private ISequence _secondSequenceSelected;
        private ISequence _firstSequenceSelected;
        private int _gapPenalty;

        public string Status
        {
            get { return _statusService.LastStatus; }
            set
            {
                _statusService.LastStatus = value;
                OnPropertyChanged("Status");
            }
        }

        public IAlignmentSequenceViewModel AlignmentSequenceViewModel
        {
            get { return _alignmentSequenceViewModel; }
        }

        public AlignViewModel(IEnumerable<IAlignSequences> sequencesAligners,
            IStatusViewModel statusService,
            IAlignmentSequenceViewModel alignmentSequenceViewModel
            )
        {
            _statusService = statusService;
            _alignmentSequenceViewModel = alignmentSequenceViewModel;
            _statusService.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());
            _sequencesAligners = sequencesAligners;

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
            _currentAligner.GapPenalty = GapPenalty;
            _currentAligner.SimilarityMatrix = new SimilarityMatrix(_currentSimilarityMatrix);
            var _alignedMatrix = _currentAligner.Align(FirstSequenceSelected, SecondSequenceSelected);
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
            return true;
        }

        public Array SimilarityMatrices
        {
            get { return Enum.GetValues(typeof (SimilarityMatrix.StandardSimilarityMatrix)); }
        }

        public SimilarityMatrix.StandardSimilarityMatrix CurrentSimilarityMatrix
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
            get { return _sequencesAligners.ToList(); }
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
                OnPropertyChanged("FirstSequenceSelected");
            }
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
                OnPropertyChanged("SecondSequenceSelected");
            }
        }
    }
}