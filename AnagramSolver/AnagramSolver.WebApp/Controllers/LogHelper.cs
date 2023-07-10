﻿using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.WebApp.Controllers
{
	public class LogHelper : ILogHelper
	{
		private readonly IWordRepository _wordRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LogHelper(IWordRepository wordRepository, IHttpContextAccessor httpContextAccessor)
		{
			_wordRepository = wordRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public void LogSearch(string inputWord)
		{
			string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

			_wordRepository.LogSearchInfo(new SearchLogDto(ipAddress, DateTime.Now, inputWord));
		}
	}
}