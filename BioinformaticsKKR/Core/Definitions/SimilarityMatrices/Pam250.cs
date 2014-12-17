using Bio;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Core.Definitions.SimilarityMatrices
{
    public class Pam250 : IAmSimilarityMatrix
    {
        public SimilarityMatrix.StandardSimilarityMatrix Matrix { get; set; }

        public Pam250()
        {
            Matrix=SimilarityMatrix.StandardSimilarityMatrix.Pam250;
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