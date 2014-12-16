using System;
using System.Collections.Generic;
using System.IO;
using Bio;
using BioinformaticsKKR.Core.Definitions;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Converter;

namespace BioinformaticsKKR.ViewModel
{
    

    public class CurrentSequenceViewModel : ViewModelBase, 
        BioinformaticsKKR.ViewModel.ICurrentSequenceViewModel
    {
        private ISequence _sequence;
        private SequenceType _currentAlphabet;
        private readonly ISequenceFileWriter _sequenceFileWriter;
        private readonly IEnumerable<ISequenceConverter> _sequenceConverters;
        private readonly IAmFileDialog _writeFileDialog;
        private ISequenceConverter _currentSequenceConverter;
        private string _filePath;
        private readonly IStatusViewModel _statusService;

        public IEnumerable<ISequenceConverter> Alphabets
        {
            get { return _sequenceConverters; }
        }

        public ISequenceConverter CurrentAlphabet
        {
            get { return _currentSequenceConverter; }
            set
            {
                _currentSequenceConverter = value;
                ConvertSequence.UpdateCanExecuteState();
                OnPropertyChanged("CurrentAlphabet");
            }
        }

        public ISequence Sequence
        {
            get { return _sequence; }
            set
            {
                _sequence = value;
                OnPropertyChanged("Sequence");
            }
        }

        public string Status
        {
            get { return _statusService.LastStatus; }
            set
            {
                _statusService.LastStatus = value;
                OnPropertyChanged("Status");
            }
        }

        public CurrentSequenceViewModel(
            ISequenceFileWriter sequenceFileWriter, 
            IEnumerable<ISequenceConverter> sequenceConverters,
            IAmFileDialog writeFileDialog,
            IStatusViewModel statusService)
        {
            _sequenceConverters = sequenceConverters;
            _writeFileDialog = writeFileDialog;
            _sequenceFileWriter = sequenceFileWriter;
            _statusService = statusService;
            _statusService.PropertyChanged += (sender, e) => { OnPropertyChanged(e.ToString()); };

            ConvertSequence = new CommandBase
            {
                CanExecuteMethod = CanConvertSequence,
                ExecuteMethod = ConvertMethod
            };

            Browse = new CommandBase
            {
                CanExecuteMethod = o => true,
                ExecuteMethod = BrowseExecuteMethod
            };
        }

        public CommandBase Browse { get; set; }

        public void BrowseExecuteMethod(object obj)
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
                    ConvertSequence.UpdateCanExecuteState();
                    OnPropertyChanged("FilePath");
                }
            }
        }

        private void ConvertMethod(object obj)
        {
            var sequence = _currentSequenceConverter.Convert(Sequence);
            _sequenceFileWriter.WriteSequence(sequence, FilePath);
            Status = string.Format("Converted {0} to {1}. Written to file.", Sequence.Alphabet.Name,
                _currentSequenceConverter.DestinationSequenceType);
        }

        private bool CanConvertSequence(object obj)
        {
            if (Sequence == null 
                || _currentSequenceConverter == null
                || string.IsNullOrEmpty(FilePath))
            {
                return false;
            }

            SequenceType seqType;
            var parseResult = Enum.TryParse(Sequence.Alphabet.Name, true, out seqType);

            if (!parseResult)
            {
                return false;
            }

            return _currentSequenceConverter.CanConvertFrom(seqType);
        }

        public CommandBase ConvertSequence { get; set; }
    }
}
