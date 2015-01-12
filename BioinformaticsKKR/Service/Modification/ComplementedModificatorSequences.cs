using Bio;

namespace BioinformaticsKKR.Service.Modification
{
    public class ComplementedModificatorSequences : IModificatorSequences
    {
        public ISequence Modify(ISequence sequence)
        {
            return sequence.GetComplementedSequence();
        }

        public override string ToString()
        {
            return "Complemented Sequence";
        }
    }
}