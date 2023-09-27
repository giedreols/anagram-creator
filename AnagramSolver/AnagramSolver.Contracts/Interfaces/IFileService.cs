namespace AnagramSolver.Contracts.Interfaces
{
    public interface IFileService
    {
        Task<byte[]> GetFileWithWordsAsync();
    }
}