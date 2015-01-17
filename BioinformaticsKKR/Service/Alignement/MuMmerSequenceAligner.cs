using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.MUMmer;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Alignement
{
    public class MuMmerSequenceAligner : IAlignSequences
    {
        public IEnumerable<IPairwiseSequenceAlignment> Align(params ISequence[] sequences)
        {
            var aligner = new MUMmerAligner
            {
                GapOpenCost = GapPenalty,
                SimilarityMatrix = SimilarityMatrix,
            };
            return aligner.Align(sequences);
        }

        public SimilarityMatrix SimilarityMatrix { get; set; }

        public int GapPenalty { get; set; }

        public bool CanAlignSequences(ISequence sequenceA, ISequence sequenceB)
        {
            if (sequenceA == null || sequenceB == null)
                return false;

            return true;
        }

        public AlignmentType AlignmentType { get; private set; }

        public MuMmerSequenceAligner()
        {
            AlignmentType = AlignmentType.Both;
        }

        public override string ToString()
        {
            return "MuMmer Aligner";
        }
    }
}