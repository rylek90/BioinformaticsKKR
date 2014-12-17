using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Modification
{
    public interface IModificatorSequences
    {
        ISequence Modify(ISequence sequence);
        string ToString();
    }
}