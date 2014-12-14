using BioinformaticsKKR.IO;
using BioinformaticsKKR.Service;
using StructureMap.Configuration.DSL;

namespace BioinformaticsKKR.Core.DependencyInjection
{
    public class BioRegistry : Registry
    
    {
        public BioRegistry()
        {
            For<ISequenceFileReader>().Use<SequenceFileReader>();
            For<ISequenceFileWriter>().Use<SequenceFileWriter>();
            For<ISequenceConverter>().Use<SequenceConverter>();
        }
    }
}