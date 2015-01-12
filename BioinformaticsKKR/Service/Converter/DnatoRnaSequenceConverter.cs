using Bio;
using Bio.Algorithms.Translation;
using BioinformaticsKKR.Core.Definitions;

namespace BioinformaticsKKR.Service.Converter
{
    public class DnatoRnaSequenceConverter : ISequenceConverter
    {
        public ISequence Convert(ISequence sequence)
        {
            return Transcription.Transcribe(sequence);
        }

        public bool CanConvertFrom(SequenceType baseType)
        {
            return baseType == SequenceType.Dna;
        }

        public SequenceType DestinationSequenceType
        {
            get { return SequenceType.Rna; }
        }

        public override string ToString()
        {
            return DestinationSequenceType.ToString();
        }
    }
}