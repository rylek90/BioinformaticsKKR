using System.Windows.Controls;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for AlignView.xaml
    /// </summary>
    public partial class AlignView : Page
    {
        private IAlignViewModel _viewModel;

        public AlignView()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IAlignViewModel>();
            _viewModel = MainGrid.DataContext as IAlignViewModel;
        }
    }
}
