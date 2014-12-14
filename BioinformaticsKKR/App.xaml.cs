using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using BioinformaticsKKR.IO;
using StructureMap;

namespace BioinformaticsKKR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<IFastaFileReader>().Use<FastaFileReader>();
            });
        }
    }
}
