using System.Collections.Generic;
using System.Linq;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Assembly;

namespace BioinformaticsKKR.ViewModel
{
    public class AssembleViewModel : ViewModelBase, BioinformaticsKKR.ViewModel.IAssembleViewModel
    {
        private readonly IEnumerable<IAssembleSequences> _sequencesAssemblers;
        private IAssembleSequences _currentAssembler;
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

        public AssembleViewModel(IEnumerable<IAssembleSequences> sequencesAssemblers,
             IStatusViewModel statusService
            )
        {
            _statusService = statusService;
            _statusService.PropertyChanged += (sender, e) => { OnPropertyChanged(e.ToString()); };
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