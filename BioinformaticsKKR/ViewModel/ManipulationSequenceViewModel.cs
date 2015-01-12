using System.Collections.ObjectModel;
using System.ComponentModel;
using Bio;
using BioinformaticsKKR.Core.Extensions;
using BioinformaticsKKR.Core.ViewModel;

namespace BioinformaticsKKR.ViewModel
{
    public interface IManipulationSequenceViewModel
    {
        ISequence SequenceA { get; set; }
        ObservableCollection<char> FirstSequence { get; set; }
        ObservableCollection<char> ModificatedSequence { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class ManipulationSequenceViewModel : ViewModelBase, IManipulationSequenceViewModel
    {
        private ObservableCollection<char> _firstSequence;
        private ObservableCollection<char> _modificatedSequence;
        private ISequence _sequenceA;


        public ISequence SequenceA
        {
            get { return _sequenceA; }
            set
            {
                if (value == null)
                    return;

                _sequenceA = value;
                FirstSequence = new ObservableCollection<char>(value.ToCharArray());
                OnPropertyChanged("SequenceA");
            }
        }

        public ObservableCollection<char> FirstSequence
        {
            get { return _firstSequence; }
            set
            {
                _firstSequence = value;
                OnPropertyChanged("FirstSequence");
            }
        }

        public ObservableCollection<char> ModificatedSequence
        {
            get { return _modificatedSequence; }
            set
            {
                _modificatedSequence = value;
                OnPropertyChanged("ModificatedSequence");
            }
        }
    }
}