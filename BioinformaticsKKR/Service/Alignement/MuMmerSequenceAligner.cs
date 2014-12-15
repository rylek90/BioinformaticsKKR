using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.MUMmer;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Alignement
{
    public class MuMmerSequenceAligner : IAlignSequences
    {
        public IEnumerable<IPairwiseSequenceAlignment> Align(ISequence sequenceA, ISequence sequenceB)
        {
            var aligner = new MUMmerAligner
            {
                GapOpenCost = GapPenalty,
                SimilarityMatrix = SimilarityMatrix,
            };

            return aligner.Align(new List<ISequence> {sequenceA, sequenceB});
        }

        public SimilarityMatrix SimilarityMatrix { get; set; }

        public int GapPenalty { get; set; }
    }
}