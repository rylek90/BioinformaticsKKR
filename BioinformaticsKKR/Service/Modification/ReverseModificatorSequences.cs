using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Modification
{
    public class ReverseModificatorSequences : IModificatorSequences
    {
        public ISequence Modify(ISequence sequence)
        {
            return sequence.GetReversedSequence();
        }

        public override string ToString()
        {
            return "Reversed Sequence";
        }
    }
}