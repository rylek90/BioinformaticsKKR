using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for ManipulationView.xaml
    /// </summary>
    /// 
    
    public partial class ManipulationView : Page
    {
        private IManipulationViewModel _viewModel;
        public ManipulationView()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IManipulationViewModel>();
            _viewModel = MainGrid.DataContext as IManipulationViewModel;
        }
    }
}
