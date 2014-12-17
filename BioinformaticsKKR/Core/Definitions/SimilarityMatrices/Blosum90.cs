using Bio;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Core.Definitions.SimilarityMatrices
{
    public class Blosum90 : IAmSimilarityMatrix
    {
        public SimilarityMatrix.StandardSimilarityMatrix Matrix { get; set; }

        public Blosum90()
        {
            Matrix = SimilarityMatrix.StandardSimilarityMatrix.Blosum90;
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