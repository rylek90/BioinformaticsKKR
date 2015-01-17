using System.Collections.Generic;
using System.Linq;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.Alignment.MultipleSequenceAlignment;
using Bio.SimilarityMatrices;
using ISequenceAlignment = Bio.Algorithms.Alignment.ISequenceAlignment;
using SequenceAlignment = Bio.Algorithms.Alignment.SequenceAlignment;

namespace BioinformaticsKKR.Service.Alignement
{
    public class PamsamMultipleSequenceAligner
    {
        public IList<ISequenceAlignment> Align(IAlphabet alphabet, params ISequence[] sequences)
        {
            var aligner = new PAMSAMMultipleSequenceAligner(
                sequences.ToList(),
                3,
                DistanceFunctionTypes.EuclideanDistance,
                UpdateDistanceMethodsTypes.Average,
                ProfileAlignerNames.NeedlemanWunschProfileAligner,
                ProfileScoreFunctionNames.WeightedInnerProduct,
                SimilarityMatrix,
                -4,
                -1,
                8,
                4);

            var alignment = new SequenceAlignment();
            var alignedSequenceItem = new AlignedSequence();

            foreach (ISequence sequence in aligner.AlignedSequencesC)
            {
                alignedSequenceItem.Sequences.Add(sequence);
            }

            alignment.AlignedSequences.Add(alignedSequenceItem);




            var alignments = aligner.Align(sequences);
            //var test = alignments.First().AlignedSequences.First().Sequences.First
            return alignments;
        }

        public PamsamMultipleSequenceAligner()
        {
            AlignmentType = AlignmentType.Multi;
        }

        public SimilarityMatrix SimilarityMatrix { get; set; }
        public int GapPenalty { get; set; }

        public bool CanAlignSequences(ISequence sequenceA, ISequence sequenceB)
        {
            return (sequenceA != null && sequenceB != null);
        }

        public AlignmentType AlignmentType { get; private set; }
    }
}