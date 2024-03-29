﻿using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordLogService
    {
        Task<int> LogWordAsync(int id, WordOpEnum action);
    }
}