using System;
using System.Collections.Generic;
using System.Linq;
using Bio.SimilarityMatrices;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Alignement;

namespace BioinformaticsKKR.ViewModel
{
    public interface IAlignViewModel
    {
        string LastStatus { get; set; }
    }

    public class AlignViewModel : ViewModelBase, IAlignViewModel
    {
        private readonly IEnumerable<IAlignSequences> _sequencesAligners;
        private IAlignSequences _currentAligner;
        private SimilarityMatrix.StandardSimilarityMatrix _currentSimilarityMatrix;
        private string _lastStatus;

        public AlignViewModel(IEnumerable<IAlignSequences> sequencesAligners)
        {
            _sequencesAligners = sequencesAligners;
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

        public string LastStatus
        {
            get { return _lastStatus; }
            set { _lastStatus = value; OnPropertyChanged("LastStatus"); }
        }
    }
}