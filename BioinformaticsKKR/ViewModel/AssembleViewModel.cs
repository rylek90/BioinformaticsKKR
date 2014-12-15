using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Assembly;

namespace BioinformaticsKKR.ViewModel
{
    public interface IAssembleViewModel
    {
    }

    public class AssembleViewModel : ViewModelBase, IAssembleViewModel
    {
        private readonly IEnumerable<IAssembleSequences> _sequencesAssemblers;
        private IAssembleSequences _currentAssembler;

        public AssembleViewModel(IEnumerable<IAssembleSequences> sequencesAssemblers)
        {
            _sequencesAssemblers = sequencesAssemblers;
        }

        public List<IAssembleSequences> SequencesAssemblers
        {
            get { return _sequencesAssemblers.ToList(); }
        }

        public IAssembleSequences CurrentAssembler
        {
            get { return _currentAssembler; }
            set
            {
                _currentAssembler = value;
                OnPropertyChanged("CurrentAssembler");
            }
        }
    }
}