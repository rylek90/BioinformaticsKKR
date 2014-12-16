using System;
namespace BioinformaticsKKR.ViewModel
{
    interface IReadFileViewModel
    {
        bool AppendToCollection { get; set; }
        bool OverwriteCollection { get; set; }
        string FilePath { get; set; }
        string Status { get; set; }
        BioinformaticsKKR.Core.ViewModel.CommandBase Browse { get; set; }
        BioinformaticsKKR.Core.ViewModel.CommandBase ReadFile { get; set; }
    }
}
