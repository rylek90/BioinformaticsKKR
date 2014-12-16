using BioinformaticsKKR.Core.ViewModel;

namespace BioinformaticsKKR.ViewModel
{
    
    public class AlignmentSequenceViewModel : ViewModelBase
    {
        private char[] _firstSequence;
        private char[] _secondSequence;
        private char[] _thirdSequence;


        public AlignmentSequenceViewModel()
        {
            FirstSequence = "AGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTU".ToCharArray();
            SecondSequence = "AGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTU".ToCharArray();
            ThirdSequence = "AGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTUAGCTU".ToCharArray();
        }


        public char[] FirstSequence
        {
            get { return _firstSequence; }
            set { _firstSequence = value; OnPropertyChanged("FirstSequence"); }
        }

        public char[] SecondSequence
        {
            get { return _secondSequence; }
            set { _secondSequence = value; OnPropertyChanged("SecondSequence"); }
        }

        public char[] ThirdSequence
        {
            get { return _thirdSequence; }
            set { _thirdSequence = value; OnPropertyChanged("ThirdSequence"); }
        }
    }
}
