using Microsoft.Win32;

namespace BioinformaticsKKR.Core.IO
{
    public class ReadFileDialog : IAmFileDialog
    {
        private readonly OpenFileDialog _openFileDialog;

        public ReadFileDialog()
        {
            _openFileDialog = new OpenFileDialog()
            {
                Filter = IoResources.ExtensionFilter
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