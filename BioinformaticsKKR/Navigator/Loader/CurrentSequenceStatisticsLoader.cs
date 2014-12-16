using BioinformaticsKKR.View;
using FirstFloor.ModernUI.Windows;

namespace BioinformaticsKKR.Navigator.Loader
{
    public class CurrentSequenceStatisticsLoader : DefaultContentLoader
    {
        protected override object LoadContent(System.Uri uri)
        {
            var sequenceId = uri.ToString().Split('#')[1];
            return new CurrentSequenceStatistics(sequenceId);
        }
    }
}