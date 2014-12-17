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
            set { _currentAligner = value; OnPropertyChanged("CurrentAligner"); }
        }

        public IEnumerable<ISequence> FirstSequencesList
        {
            get { return _firstSequencesList; }
            set
            {
                _firstSequencesList = value;
                OnPropertyChanged("FirstSequenceList");
            }
        }

        public ISequence FirstSequenceSelected
        {
            get { return _firstSequenceSelected; }
            set
            {
                _firstSequenceSelected = value;
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
                OnPropertyChanged("SecondSequenceList");
            }
        }

        public ISequence SecondSequenceSelected
        {
            get { return _secondSequenceSelected; }
            set
            {
                _secondSequenceSelected = value;
                OnPropertyChanged("SecondSequenceSelected");
            }
        }
    }
}