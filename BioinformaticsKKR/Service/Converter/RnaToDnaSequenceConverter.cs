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
            return baseType == SequenceType.RNA;
        }

        public SequenceType DestinationSequenceType
        {
            get { return SequenceType.DNA; }
        }

        public override string ToString()
        {
            return DestinationSequenceType.ToString();
        }
    }
}