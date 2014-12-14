using System.Collections.Generic;
using Bio;

namespace BioinformaticsKKR.IO
{
    public interface ISequenceFileReader
    {
        IEnumerable<ISequence> ReadSequence();
    }
}