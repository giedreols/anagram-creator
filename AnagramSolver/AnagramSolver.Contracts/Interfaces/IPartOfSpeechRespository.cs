namespace AnagramSolver.Contracts.Interfaces
{
    public interface IPartOfSpeechRespository
    {
        Task<int> InsertPartOfSpeechIfDoesNotExistAsync(string abbreviation);
    }
}
