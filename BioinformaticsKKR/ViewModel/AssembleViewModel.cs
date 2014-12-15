using System.Collections.Generic;
using System.Linq;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Assembly;

namespace BioinformaticsKKR.ViewModel
{
    public interface IAssembleViewModel
    {
        string LastStatus { get; set; }
    }

    public class AssembleViewModel : ViewModelBase, IAssembleViewModel
    {
        private readonly IEnumerable<IAssembleSequences> _sequencesAssemblers;
        private IAssembleSequences _currentAssembler;
        private string _lastStatus;

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

        public string LastStatus
        {
            get { return _lastStatus; }
            set { _lastStatus = value; OnPropertyChanged("LastStatus"); }
        }
    }
}