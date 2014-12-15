using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.IO;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.ViewModel
{
    public interface IReadFileViewModel
    {
    }

    public class ReadFileViewModel : ViewModelBase, IReadFileViewModel
    {
        public ReadFileViewModel(ISequenceFileReader fastaFileReader)
        {
            //_fastaFileReader = ContainerBootstrap.Container.GetInstance<ISequenceFileReader>();
            _fastaFileReader = fastaFileReader;

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