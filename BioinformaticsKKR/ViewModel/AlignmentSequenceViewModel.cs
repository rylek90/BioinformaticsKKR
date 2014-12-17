using System.Collections.ObjectModel;
using System.ComponentModel;
using Bio;
using BioinformaticsKKR.Core.Extensions;
using BioinformaticsKKR.Core.ViewModel;

namespace BioinformaticsKKR.ViewModel
{
    public interface IAlignmentSequenceViewModel
    {
        ISequence SequenceA { get; set; }
        ObservableCollection<char> FirstSequence { get; set; }
        ObservableCollection<char> SecondSequence { get; set; }
        ObservableCollection<char> ThirdSequence { get; set; }
        void OnPropertyChanged(string propertyName);
        event PropertyChangedEventHandler PropertyChanged;
    }

    public class AlignmentSequenceViewModel : ViewModelBase, IAlignmentSequenceViewModel
    {
        private ObservableCollection<char> _firstSequence;
        private ObservableCollection<char> _secondSequence;
        private ObservableCollection<char> _thirdSequence;
        private ISequence _sequenceA;



        public ISequence SequenceA
        {
            get { return _sequenceA; }
            set
            {
                if(value == null)
                    return;

                _sequenceA = value;
                FirstSequence = new ObservableCollection<char>(value.ToCharArray());
                OnPropertyChanged("SequenceA");
            }
        }

        public ObservableCollection<char> FirstSequence
        {
            get { return _firstSequence; }
            set { _firstSequence = value; OnPropertyChanged("FirstSequence"); }
        }

        public ObservableCollection<char> SecondSequence
        {
            get { return _secondSequence; }
            set { _secondSequence = value; OnPropertyChanged("SecondSequence"); }
        }

        public ObservableCollection<char> ThirdSequence
        {
            get { return _thirdSequence; }
            set { _thirdSequence = value; OnPropertyChanged("ThirdSequence"); }
        }
    }
}
