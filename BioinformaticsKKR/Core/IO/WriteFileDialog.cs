using Microsoft.Win32;

namespace BioinformaticsKKR.Core.IO
{
    public class WriteFileDialog : IAmFileDialog
    {
        private readonly SaveFileDialog _saveFileDialog;

        public WriteFileDialog()
        {
            _saveFileDialog = new SaveFileDialog
            {
                AddExtension = true,
                Filter = IoResources.ExtensionFilter
            };
        }

        public string FileName
        {
            get
            {
                var result = _saveFileDialog.ShowDialog();
                if (result == true)
                {
                    return _saveFileDialog.FileName;
                }

                return string.Empty;
            }
        }
    }
}