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
            if (_viewModel != null)
                _viewModel.PropertyChanged += OnPropChanged;
        }

        private void OnPropChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Aligned")
            {
                AlignmentControl.Update(_viewModel.FirstSequenceSelected, _viewModel.SecondSequenceSelected, _viewModel.Aligned);
            }
        }


        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {

        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.FirstSequencesList = SequencesRepository.Instance.Sequences;
            _viewModel.SecondSequencesList = SequencesRepository.Instance.Sequences;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AlignmentControl.Update(_viewModel.FirstSequenceSelected, _viewModel.SecondSequenceSelected);
        }
    }
}
