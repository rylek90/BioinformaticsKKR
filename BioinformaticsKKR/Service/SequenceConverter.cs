using Bio;
using Bio.Algorithms.Translation;
using BioinformaticsKKR.Core.Definitions;

namespace BioinformaticsKKR.Service
{
    public interface ISequenceConverter
    {
        ISequence Convert(ISequence sequence, SequenceType destinationType);
    }

    public class SequenceConverter : ISequenceConverter
    {
        public ISequence Convert(ISequence sequence, SequenceType destinationType)
        {
            switch (destinationType)
            {
                case SequenceType.DNA:
                    return Transcription.ReverseTranscribe(sequence);
                case SequenceType.RNA:
                    return Transcription.Transcribe(sequence);
                default:
                    return null;
            }
        }
    }
}
