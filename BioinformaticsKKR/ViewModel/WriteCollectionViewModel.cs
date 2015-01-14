using System.ComponentModel;
using System.Linq;
using System.Windows.Interop;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.ViewModel
{
    public interface IWriteCollectionViewModel
    {
        string FilePath { get; set; }
        CommandBase Browse { get; set; }
        CommandBase WriteFile { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class WriteCollectionViewModel : ViewModelBase, IWriteCollectionViewModel
    {
        private readonly ISequenceFileWriter _sequenceFileWriter;
        private readonly IAmFileDialog _writeFileDialog;
        private string _filePath;

        public WriteCollectionViewModel(ISequenceFileWriter sequenceFileWriter, IAmFileDialog writeFileDialog, IStatusViewModel statusViewModel)
        {
            _sequenceFileWriter = sequenceFileWriter;
            _writeFileDialog = writeFileDialog;

            WriteFile = new CommandBase
            {
                CanExecuteMethod = CanWriteFile,
                ExecuteMethod = WriteFileExecuteMethod
            };

            Browse = new CommandBase
            {
                CanExecuteMethod = o => true,
                ExecuteMethod = BrowseExecuteMethod
            };
        }

        private void BrowseExecuteMethod(object obj)
        {
            FilePath = _writeFileDialog.FileName;
        }

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _filePath = value;
                    WriteFile.UpdateCanExecuteState();
                    OnPropertyChanged("FilePath");
                }
            }
        }

        public CommandBase Browse { get; set; }

        public CommandBase WriteFile { get; set; }

        private void WriteFileExecuteMethod(object obj)
        {
            var sequences = SequencesRepository.Instance.Sequences.ToList();
            _sequenceFileWriter.WriteSequence(FilePath, sequences.ToArray());
        }

        private bool CanWriteFile(object obj)
        {
            return !string.IsNullOrEmpty(FilePath) 
                && SequencesRepository.Instance.Sequences.Any();
        }

    }
}