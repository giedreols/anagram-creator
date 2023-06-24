using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.DictionaryTests
{
	public class GetWordsByPageTests
	{
		private Mock<IWordRepository> _mockWordRepository;
		private WordListController _listPageController;
		private Mock<MyConfiguration> _mockConfig;

		[SetUp]
		public void Setup()
		{
			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockConfig = new Mock<MyConfiguration>(MockBehavior.Strict);
			_mockConfig.SetupAllProperties();
			_mockConfig.Object.TotalAmount = 1;
			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<WordWithFormsModel> { });
			_listPageController = new WordListController(_mockWordRepository.Object, _mockConfig.Object);
		}

		[Test]
		public void GetWordsByPage_ReturnsCurrentPageWordsInSecondPage()
		{
			var page = 2;
			var wordB = "wordB";

			_mockWordRepository.Setup(p => p.GetWordsByPage(page, _mockConfig.Object.TotalAmount)).Returns(new WordsPerPageModel(new List<string>() { wordB },
				_mockConfig.Object.TotalAmount, 2)
			{ });

			var result = (ViewResult)_listPageController.Index(page);
			var model = result.ViewData.Model as WordListModel;

			Assert.That(model?.CurrentPageWords[0], Is.EqualTo(wordB));
		}
	}
}