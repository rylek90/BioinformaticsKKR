using System;
namespace BioinformaticsKKR.ViewModel
{
    interface IAlignViewModel
    {
        BioinformaticsKKR.Service.Alignement.IAlignSequences CurrentAligner { get; set; }
        Bio.SimilarityMatrices.SimilarityMatrix.StandardSimilarityMatrix CurrentSimilarityMatrix { get; set; }
        System.Collections.Generic.List<BioinformaticsKKR.Service.Alignement.IAlignSequences> SequencesAligners { get; }
        Array SimilarityMatrices { get; }
    }
}
