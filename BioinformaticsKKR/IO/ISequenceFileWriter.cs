using Bio;

namespace BioinformaticsKKR.IO
{
    public interface ISequenceFileWriter
    {
        void WriteSequence(ISequence sequence);
    }
}