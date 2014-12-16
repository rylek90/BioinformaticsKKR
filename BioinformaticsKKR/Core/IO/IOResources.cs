using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioinformaticsKKR.Core.IO
{
    class IOResources
    {
        public static readonly string FASTA_FILEEXTENSION = "*.fa;*.mpfa;*.fna;*.faa;*.fsa;*.fas;*.fasta";
        public static readonly string FASTQ_FILEEXTENSION = "*.fq;*.fastq";
        public static readonly string GENBANK_FILEEXTENSION = "*.gb;*.gbk;*.genbank";
        public static readonly string GFF_FILEEXTENSION = "*.gff";
        public static readonly string ExtensionFilter =
            "FastA files (." + IOResources.FASTA_FILEEXTENSION + ")|" + IOResources.FASTA_FILEEXTENSION
        + "|FastQ files (." + IOResources.FASTQ_FILEEXTENSION + ")|" + IOResources.FASTQ_FILEEXTENSION
    + "|GenBank files (." + IOResources.GENBANK_FILEEXTENSION + ")|" + IOResources.GENBANK_FILEEXTENSION
    + "|GFF files (." + IOResources.GFF_FILEEXTENSION + ")|" + IOResources.GFF_FILEEXTENSION;
         
    }
}
