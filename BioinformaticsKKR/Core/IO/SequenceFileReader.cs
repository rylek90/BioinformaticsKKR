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
            List<ISequence> sequences;

            using (var inputFile = new StreamReader(path))
            {
                var type = SequenceParsers.FindParserByFileName(path).GetType();
                var parser = (ISequenceParser) Activator.CreateInstance(type);

                sequences = parser.Parse(inputFile).ToList();
            }

            return sequences;
        }
    }
}