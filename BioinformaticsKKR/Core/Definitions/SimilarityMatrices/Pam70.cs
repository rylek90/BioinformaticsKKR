using Bio;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Core.Definitions.SimilarityMatrices
{
    public class Pam70 : IAmSimilarityMatrix
    {
        public SimilarityMatrix.StandardSimilarityMatrix Matrix { get; set; }

        public Pam70()
        {
            Matrix=SimilarityMatrix.StandardSimilarityMatrix.Pam70;
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