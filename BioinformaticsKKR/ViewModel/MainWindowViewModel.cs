using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Bio;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.IO;
using StructureMap;

namespace BioinformaticsKKR.ViewModel
{

    public class MainWindowViewModel : ViewModelBase
    {
        #region Ctor
        public MainWindowViewModel()
        {
            _fastaFileReader = ObjectFactory.GetInstance<IFastaFileReader>();
            ReadFile = new CommandBase
            {
                CanExecuteMethod = o => true,
                ExecuteMethod = ReadFileExecuteMethod
            };
        }

        #endregion

        #region Private Fields
        private readonly IFastaFileReader _fastaFileReader;
        private IEnumerable<ISequence> _sequences;
        private ISequence _currentSequence;

        #endregion

        #region View Model Properties
        public IEnumerable<ISequence> Sequences
        {
            get { return _sequences; }
            set
            {
                _sequences = value;
                OnPropertyChanged("Sequences");
            }
        }

        public ISequence CurrentSequence
        {
            get { return _currentSequence; }
            set
            {
                _currentSequence = value;
                OnPropertyChanged("CurrentSequence");
            }
        }

        #endregion


        #region Command Fields
        public CommandBase ReadFile { get; set; }

        #endregion

        private void ReadFileExecuteMethod(object obj)
        {
            Sequences = _fastaFileReader.ReadSequence();
        }
    }
}