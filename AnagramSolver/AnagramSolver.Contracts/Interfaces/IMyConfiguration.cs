using AnagramSolver.Contracts.Dtos;
using Microsoft.Extensions.Configuration;

namespace AnagramSolver.BusinessLogic
{
    public interface IMyConfiguration
    {
        ConfigOptionsDto? ConfigOptions { get; set; }
    }
}