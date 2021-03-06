﻿using System.Windows.Controls;
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
        private readonly ISequencesLinksViewModel _sequencesViewModel;

        public ReadFileView()
        {
            InitializeComponent();

            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IReadFileViewModel>();
            SequencesTab.DataContext = ContainerBootstrap.Container.GetInstance<ISequencesLinksViewModel>();
            SaveCollectionGrid.DataContext = ContainerBootstrap.Container.GetInstance<IWriteCollectionViewModel>();
            _sequencesViewModel = SequencesTab.DataContext as ISequencesLinksViewModel;
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