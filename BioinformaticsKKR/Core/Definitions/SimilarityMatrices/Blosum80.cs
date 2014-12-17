using Bio;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Core.Definitions.SimilarityMatrices
{
    public class Blosum80 : IAmSimilarityMatrix
    {
        public SimilarityMatrix.StandardSimilarityMatrix Matrix { get; set; }

        public Blosum80()
        {
            Matrix=SimilarityMatrix.StandardSimilarityMatrix.Blosum80;
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