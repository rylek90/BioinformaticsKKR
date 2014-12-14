using StructureMap;
using StructureMap.Configuration.DSL;

namespace BioinformaticsKKR.Core.DependencyInjection
{
    public class ContainerBootstrap : Container
    {
        private static ContainerBootstrap _containerBootstrap;

        private ContainerBootstrap(Registry registry) : base(registry)
        {
            
        }

        public static ContainerBootstrap Container
        {
            get { return _containerBootstrap ?? (_containerBootstrap = new ContainerBootstrap(new BioRegistry())); }
        }
    }
}