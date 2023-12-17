using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IMyConfiguration
    {
        ConfigOptionsDto? ConfigOptions { get; set; }
    }
}