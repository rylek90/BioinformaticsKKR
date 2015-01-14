using System.ComponentModel;
using System.Windows.Controls;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Provider;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for AlignView.xaml
    /// </summary>
    public partial class MultiAlignView : Page, IContent
    {
        private readonly IMultiAlignViewModel _viewModel;

        public MultiAlignView()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IMultiAlignViewModel>();
            _viewModel = MainGrid.DataContext as IMultiAlignViewModel;

            if (_viewModel != null)
                _viewModel.PropertyChanged += OnPropChanged;
        }

        private void OnPropChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Aligned")
            {
                AlignmentControl.Update(_viewModel.FirstSequenceSelected, _viewModel.SecondSequenceSelected,
                    _viewModel.Aligned);
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