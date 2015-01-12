using Bio;
using System.Collections.ObjectModel;

namespace BioinformaticsKKR.Provider
{
    public class SequencesRepository
    {
        private static SequencesRepository _instance;

        public ObservableCollection<ISequence> Sequences { get; private set; }

        public int CurrentSequenceId { get; set; }

        public ISequence CurrentSequence
        {
            get { return Sequences[CurrentSequenceId]; }
            set { CurrentSequenceId = Sequences.IndexOf(value); }
        }

        private SequencesRepository()
        {
            Sequences = new ObservableCollection<ISequence>();
        }

        public static SequencesRepository Instance
        {
            get { return _instance ?? (_instance = new SequencesRepository()); }
        }
    }
}