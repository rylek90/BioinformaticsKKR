using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IManipulationSequenceViewModel _viewModel;

        public ManipulationSequenceViewControl()
        {
            InitializeComponent();
            MainGrid.DataContext = ContainerBootstrap.Container.GetInstance<IManipulationSequenceViewModel>();
            _viewModel = MainGrid.DataContext as IManipulationSequenceViewModel;
        }


        public void Update(ISequence first, ISequence second, ISequence third = null)
        {
            if (first !=null)
                _viewModel.FirstSequence = new ObservableCollection<char>(first.ToCharArray());

            if(second!=null)
            _viewModel.SecondSequence = new ObservableCollection<char>(second.ToCharArray());

            _viewModel.ThirdSequence = third == null ? null : new ObservableCollection<char>(third.ToCharArray());
        }
    }
}
