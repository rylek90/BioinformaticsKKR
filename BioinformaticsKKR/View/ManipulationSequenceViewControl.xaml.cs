using System.Collections.ObjectModel;
using System.Windows.Controls;
using Bio;
using BioinformaticsKKR.Core.DependencyInjection;
using BioinformaticsKKR.Core.Extensions;
using BioinformaticsKKR.ViewModel;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for AlignmentSequenceViewControl.xaml
    /// </summary>
    public partial class ManipulationSequenceViewControl : UserControl
    {
        private readonly IManipulationSequenceViewModel _viewModel;

        public ManipulationSequenceViewControl()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IManipulationSequenceViewModel>();
            _viewModel = MainGrid.DataContext as IManipulationSequenceViewModel;
        }


        public void Update(ISequence first, ISequence third = null)
        {
            if (first != null)
                _viewModel.FirstSequence = new ObservableCollection<char>(first.ToCharArray());

            _viewModel.ModificatedSequence = third == null ? null : new ObservableCollection<char>(third.ToCharArray());
        }
    }
}