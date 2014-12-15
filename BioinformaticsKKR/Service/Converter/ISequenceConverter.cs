using Bio;
using BioinformaticsKKR.Core.Definitions;

namespace BioinformaticsKKR.Service.Converter
{
    public interface ISequenceConverter
    {
        ISequence Convert(ISequence sequence);

        bool CanConvertFrom(SequenceType baseType);

        SequenceType DestinationSequenceType { get; }

        string ToString();
    }
}
