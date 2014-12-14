using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bio;
using Bio.IO.FastA;
using Microsoft.Win32;

namespace BioinformaticsKKR.IO
{
    public interface IFastaFileReader
    {
        IEnumerable<ISequence> ReadSequence();
    }

    public class FastaFileReader : IFastaFileReader
    {
        private readonly OpenFileDialog _openFileDialog;

        public FastaFileReader()
        {
            _openFileDialog = new OpenFileDialog
            {
                DefaultExt = "fasta",
                Filter = "FASTA Files (.fasta)|*.fasta"
            };
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
            throw new Exception();
        }
    }
}