﻿using Bio;
using Bio.Algorithms.Translation;
using BioinformaticsKKR.Core.Definitions;

namespace BioinformaticsKKR.Service.Converter
{
    public class DnaToProteinSequenceConverter : ISequenceConverter
    {
        public ISequence Convert(ISequence sequence)
        {
            var rnaSequence = Transcription.Transcribe(sequence);
            return ProteinTranslation.Translate(rnaSequence);
        }

        public bool CanConvertFrom(SequenceType baseType)
        {
            return baseType == SequenceType.Dna;
        }


        public override string ToString()
        {
            return DestinationSequenceType.ToString();
        }

        public SequenceType DestinationSequenceType
        {
            get { return SequenceType.Protein; }
        }
    }
}