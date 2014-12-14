using System.Collections.Generic;
using System.IO;
using Bio;
using Bio.IO.FastA;

namespace BioinformaticsKKR.IO
{
    public class SequenceFileWriter : ISequenceFileWriter
    {
        public void WriteSequence(ISequence sequence)
        {
            using (var outputFile = new FileStream("niechcemisie.fasta", FileMode.Create, FileAccess.Write))
            {
                var formater = new FastAFormatter();
                formater.Format(outputFile, new List<ISequence> {sequence });
            }
        }
    }
}