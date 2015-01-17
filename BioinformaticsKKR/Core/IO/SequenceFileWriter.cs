using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
using Bio.IO.FastA;
using Bio.Util;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.Core.IO
{
    public interface ISequenceFileWriter
    {
        bool WriteSequence(string path, params ISequence[] sequence);
    }

    public class SequenceFileWriter : ISequenceFileWriter
    {
        public bool WriteSequence(string path, params ISequence[] sequences)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            var tmpFormatter = Bio.IO.SequenceFormatters.FindFormatterByFileName(path);
            tmpFormatter.Close();

            using (var outputFile = new StreamWriter(path))
            {
                tmpFormatter.Open(outputFile);
                sequences.ForEach(tmpFormatter.Write);
                tmpFormatter.Close();
            }
            var repo = SequencesRepository.Instance.Sequences;

            foreach (var sequence in 
                sequences.Where(sequence => !repo.Contains(sequence)))
            {
                repo.Add(sequence);
            }

            return true;
        }
    }
}