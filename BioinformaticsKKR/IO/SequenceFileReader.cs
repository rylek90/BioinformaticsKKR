using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
using Bio.IO.FastA;
using Microsoft.Win32;

namespace BioinformaticsKKR.IO
{
    public class SequenceFileReader : ISequenceFileReader
    {
        private readonly OpenFileDialog _openFileDialog;

        public SequenceFileReader()
        {
            _openFileDialog = new OpenFileDialog();
        }

        public IEnumerable<ISequence> ReadSequence()
        {
            var result = _openFileDialog.ShowDialog();

            if (result == true)
            {
                IEnumerable<ISequence> sequences;
                using (var inputFile = new FileStream(_openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    var parser = new FastAParser();
                    sequences = parser.Parse(inputFile).ToList();
                }

                return sequences;
            }
            
            return new List<ISequence>();
        }
    }
}