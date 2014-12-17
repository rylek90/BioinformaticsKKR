using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Modification
{
    public class ReverseComplementedModificatorSequences : IModificatorSequences
    {
        public ISequence Modify(ISequence sequence)
        {
            return sequence.GetReverseComplementedSequence();
        }

        public override string ToString()
        {
            return "Reversed Complemented Sequence";
        }
    }
}