using System.Windows.Controls;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for ReadFileView.xaml
    /// </summary>
    public partial class ReadFileView : Page
    {
        public ReadFileView()
        {
            InitializeComponent();

            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IReadFileViewModel>();
        }
    }
}
