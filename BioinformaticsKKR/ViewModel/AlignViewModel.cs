using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class AlignViewModel : ViewModelBase, IAlignViewModel
    {
        private readonly IEnumerable<IAlignSequences> _sequencesAligners;
        private IAlignSequences _currentAligner;
        private SimilarityMatrix.StandardSimilarityMatrix _currentSimilarityMatrix;
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

        public AlignViewModel(IEnumerable<IAlignSequences> sequencesAligners,
            IStatusViewModel statusService
            )
        {
            _statusService = statusService;
            _statusService.PropertyChanged += (sender, e) => { 
                OnPropertyChanged(e.ToString());
            };
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

    }
}