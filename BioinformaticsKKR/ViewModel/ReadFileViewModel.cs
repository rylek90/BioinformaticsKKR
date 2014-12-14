using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.IO;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.ViewModel
{
    public class ReadFileViewModel : ViewModelBase
    {
        public ReadFileViewModel()
        {
            _fastaFileReader = ContainerBootstrap.Container.GetInstance<ISequenceFileReader>();

            SequencesRepository.Instance.Sequences.AddRange(_fastaFileReader.ReadSequence());

            ReadFile = new CommandBase
            {
                CanExecuteMethod = o => true,
                ExecuteMethod = ReadFileExecuteMethod
            };
        }

        #region Fields
        private readonly ISequenceFileReader _fastaFileReader;
        #endregion


        #region Commands
        public CommandBase ReadFile { get; set; }

        private void ReadFileExecuteMethod(object obj)
        {
           
        }

        #endregion
    }
}