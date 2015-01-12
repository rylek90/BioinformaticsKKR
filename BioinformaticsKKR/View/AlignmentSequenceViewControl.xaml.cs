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
    public partial class AlignmentSequenceViewControl : UserControl
    {
        private readonly IAlignmentSequenceViewModel _viewModel;

        public AlignmentSequenceViewControl()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IAlignmentSequenceViewModel>();
            _viewModel = MainGrid.DataContext as IAlignmentSequenceViewModel;
        }


        public void Update(ISequence first, ISequence second, ISequence third = null)
        {
            if (first != null)
                _viewModel.FirstSequence = new ObservableCollection<char>(first.ToCharArray());

            if (second != null)
                _viewModel.SecondSequence = new ObservableCollection<char>(second.ToCharArray());

            _viewModel.ThirdSequence = third == null ? null : new ObservableCollection<char>(third.ToCharArray());
        }
    }
}