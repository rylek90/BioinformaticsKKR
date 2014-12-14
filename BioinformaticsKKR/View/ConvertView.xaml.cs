using System.Windows.Controls;
using System.Windows.Navigation;
using BioinformaticsKKR.ViewModel;
using FirstFloor.ModernUI.Windows.Controls;

namespace BioinformaticsKKR.View
{
    /// <summary>
    /// Interaction logic for ConvertView.xaml
    /// </summary>
    public partial class ConvertView : Page
    {
        public ConvertView()
        {
            InitializeComponent();
        }

        private void ModernTab_OnSelectedSourceChanged(object sender, SourceEventArgs e)
        {
            var x = MainGrid.DataContext as CurrentSequenceViewModel;
        }
    }
}
