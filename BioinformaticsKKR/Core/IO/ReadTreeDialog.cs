using Microsoft.Win32;

namespace BioinformaticsKKR.Core.IO
{
    public class ReadTreeDialog : IAmFileDialog
    {
        private readonly OpenFileDialog _openFileDialog;

        public ReadTreeDialog()
        {
            _openFileDialog = new OpenFileDialog
            {
                Filter = IoResources.TreeFilter
            };
        }

        public string FileName
        {
            get
            {
                var result = _openFileDialog.ShowDialog();

                if (result == true)
                {
                    return _openFileDialog.FileName;
                }

                return string.Empty;
            }
        }
    }
}