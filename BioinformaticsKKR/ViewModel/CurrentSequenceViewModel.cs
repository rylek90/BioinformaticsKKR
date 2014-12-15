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
    public interface ICurrentSequenceViewModel
    {
    }

    public class CurrentSequenceViewModel : ViewModelBase, ICurrentSequenceViewModel
    {
        private ISequence _sequence;
        private SequenceType _currentAlphabet;
        private readonly ISequenceFileWriter _sequenceFileWriter;
        private readonly IEnumerable<ISequenceConverter> _sequenceConverters;
        private readonly IAmFileDialog _writeFileDialog;
        private ISequenceConverter _currentSequenceConverter;
        private string _filePath;
        private string _lastStatus;

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

        public CurrentSequenceViewModel(
            ISequenceFileWriter sequenceFileWriter, 
            IEnumerable<ISequenceConverter> sequenceConverters,
            IAmFileDialog writeFileDialog)
        {
            _sequenceConverters = sequenceConverters;
            _writeFileDialog = writeFileDialog;
            _sequenceFileWriter = sequenceFileWriter;

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
            LastStatus = string.Format("Converted {0} to {1}. Written to file.", Sequence.Alphabet.Name,
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

        public string LastStatus
        {
            get { return _lastStatus; }
            set { _lastStatus = value; OnPropertyChanged("LastStatus"); }
        }

        public CommandBase ConvertSequence { get; set; }
    }
}
