using Bio;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Core.Definitions.SimilarityMatrices
{
    public interface IAmSimilarityMatrix
    {
        string ToString();
        SimilarityMatrix.StandardSimilarityMatrix Matrix { get; set; }
        bool CanAlign(ISequence sequenceA, ISequence sequenceB);
    }
}