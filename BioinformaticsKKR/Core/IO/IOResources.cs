namespace BioinformaticsKKR.Core.IO
{
    internal class IoResources
    {
        public static readonly string FastaFileextension = "*.fa;*.mpfa;*.fna;*.faa;*.fsa;*.fas;*.fasta";
        public static readonly string FastqFileextension = "*.fq;*.fastq";
        public static readonly string GenbankFileextension = "*.gb;*.gbk;*.genbank";
        public static readonly string GffFileextension = "*.gff";

        public static readonly string NewickFileextension = "*.newick";

        public static readonly string ExtensionFilter =
            "FastA files (." + FastaFileextension + ")|" + FastaFileextension
            + "|FastQ files (." + FastqFileextension + ")|" + FastqFileextension
            + "|GenBank files (." + GenbankFileextension + ")|" + GenbankFileextension
            + "|GFF files (." + GffFileextension + ")|" + GffFileextension;


        public static readonly string TreeFilter =
            "FastA files (." + NewickFileextension + ")|" + NewickFileextension;
    }
}