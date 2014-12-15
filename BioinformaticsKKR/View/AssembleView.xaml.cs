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
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for AssembleView.xaml
    /// </summary>
    public partial class AssembleView : Page
    {
        private IAssembleViewModel _viewModel;

        public AssembleView()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IAssembleViewModel>();
            _viewModel = MainGrid.DataContext as IAssembleViewModel;
        }
    }
}
