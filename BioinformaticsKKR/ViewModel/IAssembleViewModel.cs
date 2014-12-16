using System;
namespace BioinformaticsKKR.ViewModel
{
    interface IAssembleViewModel
    {
        BioinformaticsKKR.Service.Assembly.IAssembleSequences CurrentAssembler { get; set; }
        System.Collections.Generic.List<BioinformaticsKKR.Service.Assembly.IAssembleSequences> SequencesAssemblers { get; }
    }
}
