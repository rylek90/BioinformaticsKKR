using System.Collections.Generic;
using Bio;
using Bio.Algorithms.Alignment;
using Bio.Algorithms.Assembly;

namespace BioinformaticsKKR.Service.Assembly
{
    public class OverlapDeNovoAssemblerService : IAssembleSequences
    {
        public IDeNovoAssembly Assemble(ISequence sequenceA, ISequence sequenceB)
        {
            var assembler = new OverlapDeNovoAssembler
            {
                AssumeStandardOrientation = AssumeStandardOrientation,
                ConsensusResolver = ConsensusResolver,
                OverlapAlgorithm = OverlapAlgorithm,
            };

            return assembler.Assemble(new List<ISequence> {sequenceA, sequenceB});
        }

        public ISequenceAligner OverlapAlgorithm { get; set; }


        public IConsensusResolver ConsensusResolver { get; set; }

        public bool AssumeStandardOrientation { get; set; }

        public override string ToString()
        {
            return "OverlapDeNovoAssemblerService";
        }
    }
}