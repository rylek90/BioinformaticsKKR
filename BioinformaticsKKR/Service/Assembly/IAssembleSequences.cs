using Bio;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.Assembly;

namespace BioinformaticsKKR.Service.Assembly
{
    public interface IAssembleSequences
    {
        IDeNovoAssembly Assemble(ISequence sequenceA, ISequence sequenceB);
        ISequenceAligner OverlapAlgorithm { get; set; }
        IConsensusResolver ConsensusResolver { get; set; }
        bool AssumeStandardOrientation { get; set; }
    }
}