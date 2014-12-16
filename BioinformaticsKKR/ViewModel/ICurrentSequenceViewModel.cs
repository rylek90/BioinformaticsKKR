using System;
namespace BioinformaticsKKR.ViewModel
{
    interface ICurrentSequenceViewModel
    {
        System.Collections.Generic.IEnumerable<BioinformaticsKKR.Service.Converter.ISequenceConverter> Alphabets { get; }
        BioinformaticsKKR.Core.ViewModel.CommandBase Browse { get; set; }
        BioinformaticsKKR.Core.ViewModel.CommandBase ConvertSequence { get; set; }
        BioinformaticsKKR.Service.Converter.ISequenceConverter CurrentAlphabet { get; set; }
        string FilePath { get; set; }
        Bio.ISequence Sequence { get; set; }
    }
}
