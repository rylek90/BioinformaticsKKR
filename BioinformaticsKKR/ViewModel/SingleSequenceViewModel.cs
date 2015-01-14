using System.Collections.ObjectModel;
using System.ComponentModel;
using Bio;
using BioinformaticsKKR.Core.Extensions;
using BioinformaticsKKR.Core.ViewModel;

namespace BioinformaticsKKR.ViewModel
{
    public interface ISingleSequenceViewModel
    {
        ISequence Sequence { get; set; }
        ObservableCollection<char> SequenceCollection { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class SingleSequenceViewModel : ViewModelBase, ISingleSequenceViewModel
    {
        private ObservableCollection<char> _sequenceCollection;
        private ISequence _sequence;
        private string _name;


        public ISequence Sequence
        {
            get { return _sequence; }
            set
            {
                if (value == null)
                {
                    _sequence = null;
                    SequenceCollection = null;
                    Name = string.Empty;
                    return;
                }

                if (value.Equals(_sequence))
                {
                    return;
                }

                _sequence = value;
                SequenceCollection = new ObservableCollection<char>(value.ToCharArray());
                Name = _sequence.ID;
                OnPropertyChanged("Sequence");
            }
        }

        public ObservableCollection<char> SequenceCollection
        {
            get { return _sequenceCollection; }
            set
            {
                _sequenceCollection = value;
                OnPropertyChanged("SequenceCollection");
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
    }
}