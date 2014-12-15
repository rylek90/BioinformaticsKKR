using System.Windows;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.IO;

namespace BioinformaticsKKR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            //ContainerBootstrap.Container.GetAllInstances<ISequenceFileReader>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            //App 
        }

    }
}
