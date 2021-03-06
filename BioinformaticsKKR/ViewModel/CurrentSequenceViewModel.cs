﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Bio;
using BioinformaticsKKR.Core.Definitions;
using BioinformaticsKKR.Core.IO;
using BioinformaticsKKR.Core.ViewModel;
using BioinformaticsKKR.Service.Converter;
using FirstFloor.ModernUI.Windows.Controls;

namespace BioinformaticsKKR.ViewModel
{
    public interface ICurrentSequenceViewModel
    {
        IEnumerable<ISequenceConverter> Alphabets { get; set; }
        ISequenceConverter CurrentAlphabet { get; set; }
        ISequence Sequence { get; set; }
        string Status { get; set; }
        CommandBase Browse { get; set; }
        string FilePath { get; set; }
        CommandBase ConvertSequence { get; set; }
        void BrowseExecuteMethod(object obj);
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class CurrentSequenceViewModel : ViewModelBase, ICurrentSequenceViewModel
    {
        private ISequence _sequence;
        private readonly ISequenceFileWriter _sequenceFileWriter;
        private readonly IEnumerable<ISequenceConverter> _sequenceConverters;
        private readonly IAmFileDialog _writeFileDialog;
        private ISequenceConverter _currentSequenceConverter;
        private string _filePath;
        private readonly IStatusViewModel _statusService;

        public IEnumerable<ISequenceConverter> CurrentConverters;

        public IEnumerable<ISequenceConverter> Alphabets
        {
            get { return CurrentConverters; }
            set
            {
                CurrentConverters = value;
                OnPropertyChanged("Alphabets");
            }
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

        private List<KeyValuePair<string, int>> _statisticsValues;

        public List<KeyValuePair<string, int>> StatisticsValues
        {
            get { return _statisticsValues; }
            set
            {
                _statisticsValues = value;
                OnPropertyChanged("StatisticsValues");
            }
        }

        public ISequence Sequence
        {
            get { return _sequence; }
            set
            {
                _sequence = value;
                SequenceType seqType;
                var parseStatus = Enum.TryParse(Sequence.Alphabet.Name, true, out seqType);

                if (parseStatus)
                {
                    Alphabets = _sequenceConverters.Where(x => x.CanConvertFrom(seqType));
                }
                else
                {
                    Alphabets = _sequenceConverters;
                }

                var list = new List<KeyValuePair<string, int>>();

                var mySequence = Sequence;
                var seqStat = new SequenceStatistics(mySequence);
                foreach (var letter in mySequence.Alphabet)
                {
                    var count = (int) seqStat.GetCount(letter);
                    list.Add(new KeyValuePair<string, int>(
                        Convert.ToChar(letter).ToString(CultureInfo.InvariantCulture), count));
                }
                StatisticsValues = list;

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
            _statusService.PropertyChanged += (sender, e) => OnPropertyChanged(e.ToString());

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

        public async void BrowseExecuteMethod(object obj)
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

        private async void ConvertMethod(object obj)
        {
            try
            {
                await Task.Run(() =>
                {
                    var sequence = _currentSequenceConverter.Convert(Sequence);
                    _sequenceFileWriter.WriteSequence(FilePath, sequence);
                    Status = string.Format("Converted {0} to {1}. Written to file.", Sequence.Alphabet.Name,
                        _currentSequenceConverter.DestinationSequenceType);
                });
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Warning!", MessageBoxButton.OK);
            }
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