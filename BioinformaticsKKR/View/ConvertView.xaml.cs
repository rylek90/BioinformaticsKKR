using System.Windows.Controls;
using System.Windows.Navigation;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows.Controls;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for ConvertView.xaml
    /// </summary>
    public partial class ConvertView : Page
    {
        public ConvertView()
        {
            InitializeComponent();

            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<ISequencesLinksViewModel>();
        }

    }
}
