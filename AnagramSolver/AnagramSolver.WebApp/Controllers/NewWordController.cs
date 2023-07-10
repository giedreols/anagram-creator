﻿using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class NewWordController : Controller
    {
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramGenerator _anagramSolver;
        private readonly MyConfiguration _config;
        private readonly ILogHelper _helpers;

        public NewWordController(IWordRepository wordRepository, IAnagramGenerator anagramSolver, MyConfiguration config, ILogHelper helpers)
        {
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;
            _config = config;
            _helpers = helpers;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([FromForm] string newWord, [FromForm] string newAbbreviation)
        {
            ViewData["CurrentWord"] = newWord;
            ViewData["Abbreviation"] = newAbbreviation;

            ViewData["SavedMessage"] = "Žodis išsaugotas žodyne";

            NewWordDto savedWord = _wordRepository.SaveWord(new FullWordDto(newWord) { PartOfSpeechAbbreviation = newAbbreviation }, _config.ConfigOptions);

            NewWordModel newWordModel = new()
            {
                IsSaved = savedWord.IsSaved,
                ErrorMessage = savedWord.ErrorMessage,
                AnagramWords = new AnagramWordsModel(newWord)
            };

            if (savedWord.IsSaved)
            {
                newWordModel.AnagramWords.Anagrams = _anagramSolver.GetAnagrams(newWord);
                _helpers.LogSearch(newWord);
                return View("../Home/WordWithAnagrams", newWordModel.AnagramWords);
            }
            
            return View(newWordModel);
        }
    }
}
