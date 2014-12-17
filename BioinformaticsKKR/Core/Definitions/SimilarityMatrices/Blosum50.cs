using Bio;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Core.Definitions.SimilarityMatrices
{
    public class Blosum50 : IAmSimilarityMatrix
    {
        public SimilarityMatrix.StandardSimilarityMatrix Matrix { get; set; }

        public Blosum50()
        {
            Matrix = SimilarityMatrix.StandardSimilarityMatrix.Blosum50;
        }
        public bool CanAlign(ISequence sequenceA, ISequence sequenceB)
        {
            if (sequenceA == null || sequenceB == null)
                return false;

            return true;
        }

        public override string ToString()
        {
            return Matrix.ToString();
        }
    }
}