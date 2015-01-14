using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
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

            using (var outputFile = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                //    var formater = new FastAFormatter();
                var formater = Bio.IO.SequenceFormatters.FindFormatterByFileName(path);
                formater.Format(outputFile, sequences);
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