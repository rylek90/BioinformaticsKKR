using System.IO;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.ViewModel
{
    public interface IReadFileViewModel
    {
        string LastStatus { get; set; }
    }

    public class ReadFileViewModel : ViewModelBase, IReadFileViewModel
    {
        public ReadFileViewModel(ISequenceFileReader fastaFileReader, IAmFileDialog readFileDialog)
        {
            _fastaFileReader = fastaFileReader;
            _readFileDialog = readFileDialog;

            Browse = new CommandBase
            {
                CanExecuteMethod = o => true,
                ExecuteMethod = BrowseExecuteMethod
            };

            ReadFile = new CommandBase
            {
                CanExecuteMethod = CanReadFile,
                ExecuteMethod = ReadFileExecuteMethod
            };
        }

        
        #region Fields
        private readonly ISequenceFileReader _fastaFileReader;
        private readonly IAmFileDialog _readFileDialog;
        private bool _appendToCollection;
        private bool _overwriteCollection;
        private string _filePath;
        private string _lastStatus;

        #endregion

        #region Properties

        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _filePath = value;
                    ReadFile.UpdateCanExecuteState();
                    OnPropertyChanged("FilePath");
                }
            }
        }


        public bool AppendToCollection
        {
            get { return _appendToCollection; }
            set
            {
                _appendToCollection = value;
                OnPropertyChanged("AppendToCollection");
            }
        }

        public bool OverwriteCollection
        {
            get
            {
                return _overwriteCollection;
            }
            set
            {
                _overwriteCollection = value;
                OnPropertyChanged("OverwriteCollection");
            }
        }

        #endregion

        #region Commands
        public CommandBase ReadFile { get; set; }

        private void ReadFileExecuteMethod(object obj)
        {
            if (OverwriteCollection)
            {
                SequencesRepository.Instance.Sequences.Clear();
            }

            var sequence = _fastaFileReader.ReadSequence(FilePath);
            SequencesRepository.Instance.Sequences.AddRange(sequence);

            LastStatus = string.Format("Read file. {0} collection", OverwriteCollection ? "Replaced" : "Append to");
        }

        public string LastStatus
        {
            get { return _lastStatus; }
            set { _lastStatus = value; OnPropertyChanged("LastStatus"); }
        }

        private bool CanReadFile(object obj)
        {
            return !string.IsNullOrEmpty(FilePath);
        }


        public CommandBase Browse { get; set; }

        public void BrowseExecuteMethod(object obj)
        {
            FilePath = _readFileDialog.FileName;
        }

        #endregion
    }
}