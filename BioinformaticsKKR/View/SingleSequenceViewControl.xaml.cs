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
    public partial class SingleSequenceViewControl : UserControl
    {
        private readonly ISingleSequenceViewModel _viewModel;

        public SingleSequenceViewControl()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<ISingleSequenceViewModel>();
            _viewModel = MainGrid.DataContext as ISingleSequenceViewModel;
        }


        public void Update(ISequence first)
        {
            _viewModel.Sequence = first;
        }
    }
}