using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Alignement
{
    public interface IAlignSequences
    {
        IEnumerable<IPairwiseSequenceAlignment> Align(ISequence sequenceA, ISequence sequenceB);
        SimilarityMatrix SimilarityMatrix { get; set; }
        int GapPenalty { get; set; }
        bool CanAlignSequences(ISequence sequenceA, ISequence sequenceB);
    }
}