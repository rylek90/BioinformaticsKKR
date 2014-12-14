using BioinformaticsKKR.IO;
using StructureMap.Configuration.DSL;

namespace BioinformaticsKKR.Core.DependencyInjection
{
    public class BioRegistry : Registry
    
    {
        public BioRegistry()
        {
            For<IFastaFileReader>().Use<FastaFileReader>();
        }
    }
}