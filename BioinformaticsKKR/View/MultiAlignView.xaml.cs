using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using Bio;
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
            if (e.PropertyName == "SecondSequenceSelected")
            {
                OverviewControl.Update(_viewModel.SecondSequenceSelected);
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
            _viewModel.AvailableSequencesList = new ObservableCollection<ISequence>(SequencesRepository.Instance.Sequences);
            if (_viewModel.SelectedSequencesList != null)
            {
                _viewModel.SelectedSequencesList.Clear();
            }
            else
            {
                _viewModel.SelectedSequencesList=new ObservableCollection<ISequence>();
            }
            _viewModel.SecondSequenceSelected = null;
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OverviewControl.Update(_viewModel.SecondSequenceSelected);
        }
    }
}