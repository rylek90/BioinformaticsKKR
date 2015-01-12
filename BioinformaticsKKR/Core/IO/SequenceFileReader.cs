using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
using Bio.IO;
using System;

namespace BioinformaticsKKR.Core.IO
{
    public interface ISequenceFileReader
    {
        IEnumerable<ISequence> ReadSequence(string path);
    }

    public class SequenceFileReader : ISequenceFileReader
    {
        public IEnumerable<ISequence> ReadSequence(string path)
        {
            var sequences = new List<ISequence>();

            using (var inputFile = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var type = SequenceParsers.FindParserByFileName(path).GetType();
                var parser = (ISequenceParser) Activator.CreateInstance(type);

                sequences = parser.Parse(inputFile).ToList();
            }

            return sequences;
        }
    }
}