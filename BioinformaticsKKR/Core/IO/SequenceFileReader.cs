using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
using Bio.IO.FastA;

namespace BioinformaticsKKR.Core.IO
{
    public interface ISequenceFileReader
    {
        IEnumerable<ISequence> ReadSequence(string path);

    }

    public class SequenceFileReader : ISequenceFileReader
    {

        public SequenceFileReader()
        {
        }

        public IEnumerable<ISequence> ReadSequence(string path)
        {
            var sequences = new List<ISequence>();
            
            using (var inputFile = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var parser = Bio.IO.SequenceParsers.FindParserByFileName(path);
                sequences = parser.Parse(inputFile).ToList();
            }
            
            return sequences;

        }
    }
}