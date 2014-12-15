using Bio;
using Bio.Algorithms.Translation;
using BioinformaticsKKR.Core.Definitions;

namespace BioinformaticsKKR.Service.Converter
{
    public class RnaToProteinSequenceConverter : ISequenceConverter
    {
        public ISequence Convert(ISequence sequence, SequenceType destinationType)
        {
            return ProteinTranslation.Translate(sequence);
        }

        public bool CanConvertFrom(SequenceType baseType)
        {
            return baseType == SequenceType.RNA;
        }

        public SequenceType DestinationSequenceType
        {
            get { return SequenceType.Protein; }
        }

        public override string ToString()
        {
            return DestinationSequenceType.ToString();
        }
    }
}