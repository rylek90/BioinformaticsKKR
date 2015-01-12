using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for StatisticsView.xaml
    /// </summary>
    public partial class StatisticsView : Page, IContent
    {
        private readonly ISequencesStatisticsLinksViewModel _sequencesViewModel;

        public StatisticsView()
        {
            InitializeComponent();
            SequencesTab.DataContext = ContainerBootstrap.Container.GetInstance<ISequencesStatisticsLinksViewModel>();
            _sequencesViewModel = SequencesTab.DataContext as ISequencesStatisticsLinksViewModel;
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            _sequencesViewModel.InitializeCollection();
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _sequencesViewModel.InitializeCollection();
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}