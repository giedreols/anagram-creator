using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordLogService
    {
        void LogWord(int id, string ipAddress, WordOpEnum action);
    }
}