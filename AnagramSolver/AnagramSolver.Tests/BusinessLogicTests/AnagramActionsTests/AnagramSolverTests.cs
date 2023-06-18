﻿using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.AnagramActionsTests
{
	public class AnagramSolverTests
	{
		private Mock<IWordRepository> _mockWordRepository;
		private Mock<ICachedWordRepository> _mockCachedWordRepository;
		private IAnagramGenerator _anagramSolver;

		[SetUp]
		public void Setup()
		{
			List<WordWithFormsModel> list = new() {
				new WordWithFormsModel("siela"),
				new WordWithFormsModel("alus"),
				new WordWithFormsModel("upė"),
				new WordWithFormsModel("liesa"),
				new WordWithFormsModel("liepa"),
				new WordWithFormsModel("sula") };

			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockWordRepository.Setup(p => p.GetWords()).Returns(list);

			_anagramSolver = new BusinessLogic.AnagramActions.AnagramGenerator(_mockWordRepository.Object);
		}


		[Test]
		public void GetAnagrams_ReturnsAnagram_IfInputWordHasIt()
		{
			IList<string> expResult = new List<string>() { "liepa" };

			IList<string> anagrams = _anagramSolver.GetAnagrams("palei");

			Assert.That(anagrams, Is.EqualTo(expResult));
		}

		[Test]
		public void GetAnagrams_ReturnsEmtpyList_IfInputWordDoesNotHaveIt()
		{
			IList<string> anagrams = _anagramSolver.GetAnagrams("rytas");

			Assert.That(anagrams, Is.Empty);
		}

		[Test]
		public void GetAnagrams_ReturnsAnagram_IfInputWordInCapitals()
		{
			IList<string> expResult = new List<string> { "siela" };

			IList<string> anagrams = _anagramSolver.GetAnagrams("LIESA");

			Assert.That(anagrams, Is.EqualTo(expResult));
		}

		[Test]
		public void GetAnagrams_ReturnsEmptyList_IfInputWordExistsInList()
		{
			IList<string> anagrams = _anagramSolver.GetAnagrams("upė");

			Assert.That(anagrams, Is.Empty);
		}

	}
}