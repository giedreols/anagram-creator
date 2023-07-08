﻿using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolver.Tests.WebAppTests
{
	[TestFixture]
	internal class NewWordControllerTests
	{
		private NewWordController _newWordController;
		private Mock<IWordRepository> _mockWordRepository;
		private Mock<IAnagramGenerator> _mockAnagramSolver;
		private Mock<MyConfiguration> _mockConfig;
		private Mock<IHelpers> _mockHelpers;

		[SetUp]
		public void SetUp()
		{
			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockAnagramSolver = new Mock<IAnagramGenerator>(MockBehavior.Strict);
			_mockConfig = new Mock<MyConfiguration>(MockBehavior.Strict);
			_mockHelpers = new Mock<IHelpers>(MockBehavior.Strict);
			_mockHelpers.Setup(m => m.LogSearch(It.IsAny<string>()))
						.Callback<string>(inputWord =>
						{
							string ipAddress = "127.0.0.1";
							DateTime now = DateTime.Now;
						});
			_mockConfig.SetupAllProperties();
			_mockConfig.Object.TotalAmount = 1;

			_newWordController = new NewWordController(_mockWordRepository.Object, _mockAnagramSolver.Object, _mockConfig.Object, _mockHelpers.Object);
		}

		[Test]
		public void Create_ReturnsAnagramWordsModel_IfWordIsCreated()
		{
			var word = "labas";

			_mockWordRepository.Setup(p => p.SaveWord(word)).Returns(true);
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());

			var result = (ViewResult)_newWordController.Create("labas");

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Create_ReturnsAnagramsAfterWordsCreating_IfWordHasIt()
		{
			var word = "zodis";
			var expectedResult = new List<string>() { "anagrama1", "anagrama2" };

			_mockWordRepository.Setup(p => p.SaveWord(word)).Returns(true);
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(expectedResult);
			_mockWordRepository.Setup(p => p.GetWordsByPage(1, 1)).Returns(new WordsPerPageDto(expectedResult, 1, 1));

			var result = (ViewResult)_newWordController.Create(word);
			var model = result.Model as AnagramWordsModel;

			Assert.That(model.Anagrams, Is.EqualTo(expectedResult));
		}

		[Test]
		public void Create_ReturnsEmptyListAfterWordsCreating_IfWordDoesNotHaveAnagrams()
		{
			var word = "zodis";

			_mockWordRepository.Setup(p => p.SaveWord(word)).Returns(true);
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());
			_mockWordRepository.Setup(p => p.GetWordsByPage(1, 1)).Returns(new WordsPerPageDto(new List<string>(), 1, 1));

			var result = (ViewResult)_newWordController.Create(word);
			var model = result.Model as AnagramWordsModel;

			Assert.That(model.Anagrams, Is.Empty);
		}


		[Test]
		public void Create_ReturnsViewNameEqualsToWordWithAnagrams_IfWordIsCreated()
		{
			var word = "labas";
			_mockWordRepository.Setup(p => p.SaveWord(word)).Returns(true);
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());

			var result = (ViewResult)_newWordController.Create(word);

			Assert.That(result.ViewName, Is.EqualTo("../Home/WordWithAnagrams"));
		}

		[Test]
		[TestCase(".lus")]
		[TestCase("a'us")]
		[TestCase("al?s")]
		[TestCase("alu@")]
		public void Create_ReturnsWordFailedToSaveModel_IfWordHasInvalidCharacters(string inputWord)
		{
			var result = (ViewResult)_newWordController.Create(inputWord);

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<WordFailedToSaveModel>());
		}

		[Test]
		public void Create_ReturnsWordFailedToSaveModel_IfWordIsTooShort()
		{
			_mockConfig.Object.MinLength = 5;

			var result = (ViewResult)_newWordController.Create("1234");

			Assert.That(result.Model, Is.InstanceOf<WordFailedToSaveModel>());
		}

		[Test]
		public void Create_ReturnsWordFailedToSaveModel_IfWordIsTooLong()
		{
			_mockConfig.Object.MaxLength = 2;

			var result = (ViewResult)_newWordController.Create("123");

			Assert.That(result.Model, Is.InstanceOf<WordFailedToSaveModel>());
		}

		[Test]
		public void Create_ReturnsViewNameEqualsToIndex_IfWordHasInvalidCharacters()
		{
			var result = (ViewResult)_newWordController.Create("sie.a");

			Assert.That(result, Is.Not.Null);
			Assert.That(result.ViewName, Is.EqualTo("Index"));
		}

		[Test]
		public void Create_ReturnsViewNameEqualsToIndex_IfWordIsTooShort()
		{
			_mockConfig.Object.MinLength = 5;

			var result = (ViewResult)_newWordController.Create("1234");

			Assert.That(result.ViewName, Is.EqualTo("Index"));
		}

		[Test]
		public void Create_ReturnsViewNameEqualsToIndex_IfWordIsTooLong()
		{
			_mockConfig.Object.MaxLength = 2;

			var result = (ViewResult)_newWordController.Create("123");

			Assert.That(result.ViewName, Is.EqualTo("Index"));
		}

		[Test]
		public void Create_ReturnsWordFailedToSaveModel_IfWordAlreadyExists()
		{
			var word = "labas";
			_mockWordRepository.Setup(p => p.SaveWord(word)).Returns(false);

			var result = (ViewResult)_newWordController.Create(word);

			Assert.That(result.Model, Is.InstanceOf<WordFailedToSaveModel>());
		}

		[Test]
		public void Create_ReturnsViewNameEqualsToIndex_IfWordAlreadyExists()
		{
			var word = "labas";
			_mockWordRepository.Setup(p => p.SaveWord(word)).Returns(false);

			var result = (ViewResult)_newWordController.Create(word);

			Assert.That(result.ViewName, Is.EqualTo("Index"));
		}
	}
}