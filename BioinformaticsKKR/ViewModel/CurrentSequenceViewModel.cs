using System;
using System.Collections.Generic;
using System.Windows.Documents;
using Bio;
using BioinformaticsKKR.Core.Definitions;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.IO;
using BioinformaticsKKR.Service;

namespace BioinformaticsKKR.ViewModel
{
    public class CurrentSequenceViewModel : ViewModelBase
    {
        private ISequence _sequence;
        private SequenceType _currentAlphabet;
        private ISequenceFileWriter _sequenceFileWriter;
        private ISequenceConverter _sequenceConverter;

        public Array Alphabets
        {
            get { return Enum.GetValues(typeof (SequenceType)); }
        }

        public SequenceType CurrentAlphabet
        {
            get { return _currentAlphabet; }
            set
            {
                _currentAlphabet = value;
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

        public CurrentSequenceViewModel()
        {
            _sequenceConverter = ContainerBootstrap.Container.GetInstance<ISequenceConverter>();
            _sequenceFileWriter = ContainerBootstrap.Container.GetInstance<ISequenceFileWriter>();
            ConvertSequence = new CommandBase
            {
                CanExecuteMethod = CanConvertSequence,
                ExecuteMethod = ConvertMethod
            };
        }

        private void ConvertMethod(object obj)
        {
            var convertedSequence = _sequenceConverter.Convert(Sequence, _currentAlphabet);
            _sequenceFileWriter.WriteSequence(convertedSequence);
        }

        private bool CanConvertSequence(object obj)
        {
            if (Sequence == null)
                return false;
            return Sequence.Alphabet.Name.ToLower() != _currentAlphabet.ToString().ToLower();
        }

        public CommandBase ConvertSequence { get; set; }
    }
}
