using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Provider;
using BioinformaticsKKR.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for ManipulationView.xaml
    /// </summary>
    /// 
    public partial class ManipulationView : Page, IContent
    {
        private IManipulationViewModel _viewModel;

        public ManipulationView()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IManipulationViewModel>();
            _viewModel = MainGrid.DataContext as IManipulationViewModel;
            if (_viewModel != null)
                _viewModel.PropertyChanged += OnPropChanged;
            // _viewModel.SequencesList = SequencesRepository.Instance.Sequences;
        }

        private void OnPropChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ModificatedSequence")
            {
                ManipulationControl.Update(_viewModel.SequenceSelected, _viewModel.ModificatedSequence);
            }
        }


        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            _viewModel.SequencesList = SequencesRepository.Instance.Sequences;
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.SequencesList = SequencesRepository.Instance.Sequences;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ManipulationControl.Update(_viewModel.SequenceSelected);
        }
    }
}