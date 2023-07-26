using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordLogService
    {
        void Log(int id, string ipAddress, WordOpEnum action);
    }
}