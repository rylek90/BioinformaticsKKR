using System;
namespace BioinformaticsKKR.ViewModel
{
    interface ISequencesLinksViewModel
    {
        void InitializeCollection();
        FirstFloor.ModernUI.Presentation.LinkCollection SequencesLinkCollection { get; set; }
    }
}
