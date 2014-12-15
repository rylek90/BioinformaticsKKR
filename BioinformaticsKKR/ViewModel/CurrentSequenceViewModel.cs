using System;
using System.Collections.Generic;
using System.Linq;
using Bio;
using Bio.Util;
using BioinformaticsKKR.Core.Definitions;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.IO;
using BioinformaticsKKR.Service;

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
        private ISequenceConverter _currentSequenceConverter;

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

        public CurrentSequenceViewModel(ISequenceFileWriter sequenceFileWriter, IEnumerable<ISequenceConverter> sequenceConverters)
        {
            _sequenceConverters = sequenceConverters;
            _sequenceFileWriter = sequenceFileWriter;

            ConvertSequence = new CommandBase
            {
                CanExecuteMethod = CanConvertSequence,
                ExecuteMethod = ConvertMethod
            };
        }

        private void ConvertMethod(object obj)
        {
            var convertedSequence = _currentSequenceConverter.Convert(Sequence, _currentAlphabet);
            _sequenceFileWriter.WriteSequence(convertedSequence);
        }

        private bool CanConvertSequence(object obj)
        {
            if (Sequence == null || _currentSequenceConverter == null)
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
