using System.Collections.Generic;
using System.Linq;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.Alignment.MultipleSequenceAlignment;
using Bio.SimilarityMatrices;
using BioinformaticsKKR.Core.Definitions.SimilarityMatrices;
using ISequenceAlignment = Bio.Algorithms.Alignment.ISequenceAlignment;
using SequenceAlignment = Bio.Algorithms.Alignment.SequenceAlignment;

namespace BioinformaticsKKR.Service.Alignement
{
    public interface IPamsamMultipleSequenceAligner
    {
        IList<ISequenceAlignment> Align(IAlphabet alphabet, params ISequence[] sequences);
        ProfileScoreFunctionNames ProfileScoreFunctionNames { get; set; }
        int GapExtendPenalty { get; set; }
        int NumberOfParitions { get; set; }
        int DegreeOfParallelism { get; set; }
        ProfileAlignerNames ProfileAlignerNames { get; set; }
        UpdateDistanceMethodsTypes UpdateDistanceMethodsTypes { get; set; }
        DistanceFunctionTypes DistanceFunctionTypes { get; set; }
        int KmerLength { get; set; }
        IAmSimilarityMatrix SimilarityMatrix { get; set; }
        int GapPenalty { get; set; }
        AlignmentType AlignmentType { get; }
        bool CanAlignSequences(ISequence sequenceA, ISequence sequenceB);
    }

    public class PamsamMultipleSequenceAligner : IPamsamMultipleSequenceAligner
    {

        private void DefaultValues()
        {
            KmerLength = 3;
            DistanceFunctionTypes = DistanceFunctionTypes.EuclideanDistance;
            UpdateDistanceMethodsTypes=UpdateDistanceMethodsTypes.Average;
            ProfileAlignerNames = ProfileAlignerNames.NeedlemanWunschProfileAligner;
            ProfileScoreFunctionNames = ProfileScoreFunctionNames.WeightedEuclideanDistance;
            GapPenalty = -4;
            GapExtendPenalty =- 1;
            NumberOfParitions = 8;
            DegreeOfParallelism = 4;
        }

        public IList<ISequenceAlignment> Align(IAlphabet alphabet, params ISequence[] sequences)
        {
            var aligner = new PAMSAMMultipleSequenceAligner(
                sequences.ToList(),
                KmerLength, // 3
                DistanceFunctionTypes, // Eucli
                UpdateDistanceMethodsTypes, // avg
                ProfileAlignerNames, // needleman wunch
                ProfileScoreFunctionNames,  // weighted inner
                new SimilarityMatrix(SimilarityMatrix.Matrix), // similarity matrix
                GapPenalty, // -4
                GapExtendPenalty, // -1
                NumberOfParitions,  // 8
                DegreeOfParallelism); // 4

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

        public ProfileScoreFunctionNames ProfileScoreFunctionNames { get; set; }

        public int GapExtendPenalty { get; set; }

        public int NumberOfParitions { get; set; }

        public int DegreeOfParallelism { get; set; }

        public ProfileAlignerNames ProfileAlignerNames { get; set; }

        public UpdateDistanceMethodsTypes UpdateDistanceMethodsTypes { get; set; }

        public DistanceFunctionTypes DistanceFunctionTypes { get; set; }

        public int KmerLength { get; set; }

        public PamsamMultipleSequenceAligner()
        {
            DefaultValues();
            AlignmentType = AlignmentType.Multi;
        }

        public IAmSimilarityMatrix SimilarityMatrix { get; set; }
        public int GapPenalty { get; set; }

        public bool CanAlignSequences(ISequence sequenceA, ISequence sequenceB)
        {
            return (sequenceA != null && sequenceB != null);
        }

        public AlignmentType AlignmentType { get; private set; }
    }
}