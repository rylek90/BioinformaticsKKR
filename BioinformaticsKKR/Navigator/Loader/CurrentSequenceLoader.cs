using BioinformaticsKKR.View;
using FirstFloor.ModernUI.Windows;

namespace BioinformaticsKKR.Navigator.Loader
{
    public class CurrentSequenceLoader : DefaultContentLoader
    {
        protected override object LoadContent(System.Uri uri)
        {
            var sequenceId = uri.ToString().Split('#')[1];
            return new CurrentSequence(sequenceId);
        }
    }
}