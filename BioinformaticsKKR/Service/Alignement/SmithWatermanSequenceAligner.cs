using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Alignement
{
    public class SmithWatermanSequenceAligner : IAlignSequences
    {
        public IEnumerable<IPairwiseSequenceAlignment> Align(ISequence[] sequences)
        {
            var aligner = new SmithWatermanAligner
            {
                SimilarityMatrix = SimilarityMatrix,
                GapOpenCost = GapPenalty,
            };

            return aligner.Align(sequences);
        }

        public int GapPenalty { get; set; }

        public bool CanAlignSequences(ISequence sequenceA, ISequence sequenceB)
        {
            if (sequenceA == null || sequenceB == null)
                return false;

            return true;
        }

        public AlignmentType AlignmentType { get; private set; }

        public SmithWatermanSequenceAligner()
        {
            AlignmentType = AlignmentType.Pair;
        }

        public SimilarityMatrix SimilarityMatrix { get; set; }

        public override string ToString()
        {
            return "Smith-Waterman Aligner";
        }
    }
}