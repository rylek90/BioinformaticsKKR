﻿using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.SimilarityMatrices;

namespace BioinformaticsKKR.Service.Alignement
{
    public class NeedlemanWunschSequenceAligner : IAlignSequences
    {
        public IEnumerable<IPairwiseSequenceAlignment> Align(ISequence sequenceA, ISequence sequenceB)
        {
            var aligner = new NeedlemanWunschAligner
            {
                SimilarityMatrix = SimilarityMatrix,
                GapOpenCost = GapPenalty,
            };

            return aligner.Align(sequenceA, sequenceB);
        }

        public int GapPenalty { get; set; }

        public SimilarityMatrix SimilarityMatrix { get; set; }
    }
}