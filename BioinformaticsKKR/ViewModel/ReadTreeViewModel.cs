using System.ComponentModel;
using Bio.Phylogenetics;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Core.ViewModel;

namespace BioinformaticsKKR.ViewModel
{
    public interface IReadTreeViewModel
    {
        string Status { get; set; }
        string FilePath { get; set; }
        CommandBase ReadFile { get; set; }
        CommandBase Browse { get; set; }
        void BrowseExecuteMethod(object obj);
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class ReadTreeViewModel : ViewModelBase, IReadTreeViewModel
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


        public ReadTreeViewModel(ITreeFileReader treeFileReader,
            IAmFileDialog readTreeDialog,
            IStatusViewModel statusViewModel)
        {
            _treeFileReader = treeFileReader;
            _readFileDialog = readTreeDialog;
            _statusService = statusViewModel;
            _statusService.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());
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
        }

        #region Fields

        private readonly ITreeFileReader _treeFileReader;
        private readonly IAmFileDialog _readFileDialog;
        private string _filePath;
        private Tree _tree;

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

        #endregion

        #region Commands

        public CommandBase ReadFile { get; set; }

        private void ReadFileExecuteMethod(object obj)
        {
            TreeViewModel = _treeFileReader.ReadTree(FilePath);
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

        public Tree TreeViewModel
        {
            get { return _tree; }
            set
            {
                _tree = value;
                OnPropertyChanged("TreeViewModel");
            }
        }
    }
}