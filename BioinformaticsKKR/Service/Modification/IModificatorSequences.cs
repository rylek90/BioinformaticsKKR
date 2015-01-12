using Bio;

namespace BioinformaticsKKR.Service.Modification
{
    public interface IModificatorSequences
    {
        ISequence Modify(ISequence sequence);
        string ToString();
    }
}