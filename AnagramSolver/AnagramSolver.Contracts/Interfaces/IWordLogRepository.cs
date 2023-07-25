using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.EF.DbFirst
{
    public interface IWordLogRepository
    {
        void Add(WordLogDto word);
    }
}