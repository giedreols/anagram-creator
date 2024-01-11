using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IConfigReader
    {
       public ConfigOptionsDto? ConfigOptions { get; set; }
    }
}