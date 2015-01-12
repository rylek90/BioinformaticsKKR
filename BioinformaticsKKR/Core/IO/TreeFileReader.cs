using System.IO;
using Bio.IO.Newick;
using Bio.Phylogenetics;

namespace BioinformaticsKKR.Core.IO
{
    public interface ITreeFileReader
    {
        Tree ReadTree(string path);
    }

    public class TreeFileReader : ITreeFileReader
    {
        public Tree ReadTree(string path)
        {
            Tree tree;
            using (var inputFile = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var newickParser = new NewickParser();
                tree = newickParser.Parse(inputFile);
            }

            return tree;
        }
    }
}