using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Modification
{
    public class ComplementedModificatorSequences : IModificatorSequences
    {
        public ISequence Modify(ISequence sequence)
        {
 	       return sequence.GetComplementedSequence();
        }

        public override string ToString()
        {
            return "Complemented Sequence";
        }
    }
}