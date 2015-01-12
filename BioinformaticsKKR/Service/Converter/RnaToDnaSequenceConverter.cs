using Bio;
using Bio.Algorithms.Translation;
using BioinformaticsKKR.Core.Definitions;

namespace BioinformaticsKKR.Service.Converter
{
    public class RnaToDnaSequenceConverter : ISequenceConverter
    {
        public ISequence Convert(ISequence sequence)
        {
            return Transcription.ReverseTranscribe(sequence);
        }

        public bool CanConvertFrom(SequenceType baseType)
        {
            return baseType == SequenceType.Rna;
        }

        public SequenceType DestinationSequenceType
        {
            get { return SequenceType.Dna; }
        }

        public override string ToString()
        {
            return DestinationSequenceType.ToString();
        }
    }
}