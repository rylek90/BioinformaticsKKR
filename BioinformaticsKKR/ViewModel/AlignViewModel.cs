using System.Collections.Generic;
using System.Linq;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Alignement;

namespace BioinformaticsKKR.ViewModel
{
    public interface IAlignViewModel
    {
    }

    public class AlignViewModel : ViewModelBase, IAlignViewModel
    {
        private readonly IEnumerable<IAlignSequences> _sequencesAligners;
        private IAlignSequences _currentAligner;

        public AlignViewModel(IEnumerable<IAlignSequences> sequencesAligners)
        {
            _sequencesAligners = sequencesAligners;
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