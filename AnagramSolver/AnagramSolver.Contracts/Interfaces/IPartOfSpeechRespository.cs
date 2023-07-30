namespace AnagramSolver.Contracts.Interfaces
{
    public interface IPartOfSpeechRespository
    {
        int InsertPartOfSpeechIfDoesNotExist(string abbreviation);
    }
}
