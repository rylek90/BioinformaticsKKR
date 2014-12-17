using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows;
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
using System.Windows.Shapes;
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
