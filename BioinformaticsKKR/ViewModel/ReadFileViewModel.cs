using System.IO;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.ViewModel
{
    public class ReadFileViewModel : ViewModelBase, BioinformaticsKKR.ViewModel.IReadFileViewModel
    {
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


        public ReadFileViewModel(ISequenceFileReader fastaFileReader, 
            IAmFileDialog readFileDialog, 
            IStatusViewModel statusViewModel)
        {

            _fastaFileReader = fastaFileReader;
            _readFileDialog = readFileDialog;
            _statusService = statusViewModel;
            _statusService.PropertyChanged += (sender, e) => { 
                OnPropertyChanged(e.ToString()); 
            };
            Status = "Status";

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
            AppendToCollection = true;
        }
        
        #region Fields
        private readonly ISequenceFileReader _fastaFileReader;
        private readonly IAmFileDialog _readFileDialog;
        private bool _appendToCollection;
        private bool _overwriteCollection;
        private string _filePath;

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
            foreach (var s in sequence)
            {
                SequencesRepository.Instance.Sequences.Add(s);
            }

            Status = string.Format("Read file. {0} collection", OverwriteCollection ? "Replaced" : "Append to");
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