using System.Windows.Controls;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows;
using FragmentNavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for ConvertView.xaml
    /// </summary>
    public partial class ConvertView : Page, IContent
    {
        private readonly ISequencesLinksViewModel _viewModel;

        public ConvertView()
        {
            InitializeComponent();

            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<ISequencesLinksViewModel>();
            _viewModel = MainGrid.DataContext as ISequencesLinksViewModel;
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            _viewModel.InitializeCollection();
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.InitializeCollection();
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}