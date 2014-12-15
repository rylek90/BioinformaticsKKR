using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Provider;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows;
using NavigatingCancelEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigatingCancelEventArgs;
using NavigationEventArgs = FirstFloor.ModernUI.Windows.Navigation.NavigationEventArgs;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for CurrentSequence.xaml
    /// </summary>
    public partial class CurrentSequence : UserControl, IContent
    {
        private CurrentSequenceViewModel _viewModel;

        public CurrentSequence()
        {
            InitializeComponent();

            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<ICurrentSequenceViewModel>();
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public CurrentSequence(string seq) : this()
        {
            _viewModel = MainGrid.DataContext as CurrentSequenceViewModel;

            var id = Int32.Parse(seq);

            if (_viewModel != null)
            {
                _viewModel.Sequence = SequencesRepository.Instance.Sequences[id];
            }
        }

        public void OnFragmentNavigation(FirstFloor.ModernUI.Windows.Navigation.FragmentNavigationEventArgs e)
        {
            var id = int.Parse(e.Fragment);
            _viewModel.Sequence = SequencesRepository.Instance.Sequences[id];
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            { }
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}
