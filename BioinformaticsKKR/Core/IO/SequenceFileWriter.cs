using System.Collections.Generic;
using System.IO;
using Bio;
using Bio.IO.FastA;
using BioinformaticsKKR.Provider;

namespace BioinformaticsKKR.Core.IO
{
    public interface ISequenceFileWriter
    {
        bool WriteSequence(ISequence sequence, string path);
    }

    public class SequenceFileWriter : ISequenceFileWriter
    {
        public bool WriteSequence(ISequence sequence, string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            using (var outputFile = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                //    var formater = new FastAFormatter();
                var formater = Bio.IO.SequenceFormatters.FindFormatterByFileName(path);
                formater.Format(outputFile, new List<ISequence> {sequence});
            }
            var repo = SequencesRepository.Instance.Sequences;
            if (!repo.Contains(sequence))
            {
                repo.Add(sequence);
            }

            return true;
        }
    }
}