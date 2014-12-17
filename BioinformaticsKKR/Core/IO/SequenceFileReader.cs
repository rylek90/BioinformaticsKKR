using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
using Bio.IO.FastA;
using System.Reflection;
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

        public SequenceFileReader()
        {
        }

        public IEnumerable<ISequence> ReadSequence(string path)
        {
            var sequences = new List<ISequence>();
            
            using (var inputFile = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var type = Bio.IO.SequenceParsers.FindParserByFileName(path).GetType();
                var parser = (ISequenceParser)Activator.CreateInstance(type);
                
                //var parser = new FastAParser();

                sequences = parser.Parse(inputFile).ToList();
            }
            
            return sequences;

        }
    }
}