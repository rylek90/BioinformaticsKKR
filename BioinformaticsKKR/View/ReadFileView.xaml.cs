using System.Windows.Controls;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for ReadFileView.xaml
    /// </summary>
    public partial class ReadFileView : Page, IContent
    {
        private readonly IReadFileViewModel _viewModel;

        public ReadFileView()
        {
            InitializeComponent();

            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IReadFileViewModel>();
            _viewModel = MainGrid.DataContext as IReadFileViewModel;
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            _viewModel.LastStatus = string.Empty;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
            
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.LastStatus = string.Empty;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
