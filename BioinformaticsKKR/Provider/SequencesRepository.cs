using System.Collections.Generic;
using Bio;

namespace BioinformaticsKKR.Provider
{
    public class SequencesRepository
    {
        private static SequencesRepository _instance;

        public List<ISequence> Sequences { get; private set; }

        public int CurrentSequenceId { get; set; }

        public ISequence CurrentSequence
        {
            get { return Sequences[CurrentSequenceId]; }
            set { CurrentSequenceId = Sequences.IndexOf(value); }
        }
        
        private SequencesRepository()
        {
            Sequences = new List<ISequence>();
        }

        public static SequencesRepository Instance
        {
            get { return _instance ?? (_instance = new SequencesRepository()); }
        }

    }
}