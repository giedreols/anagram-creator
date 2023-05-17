using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolver.Tests.WebAppTests
{
	[TestFixture]
	internal class ListPageControllerTests
	{
		private WordListController _listPageController;
		private Mock<IWordRepository> _mockWordRepository;
		private Mock<MyConfiguration> _mockConfig;

		[SetUp]
		public void SetUp()
		{
			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockConfig = new Mock<MyConfiguration>(MockBehavior.Strict);
			_mockConfig.SetupAllProperties();
			_mockConfig.Object.TotalAmount = 1;
			_listPageController = new WordListController(_mockWordRepository.Object, _mockConfig.Object);

		}

		[Test]
		public void Index_ReturnsListPagesModel()
		{
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("") });
			
			var result = (ViewResult)_listPageController.Index();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<WordListModel>());
		}

		[Test]
		public void Index_ReturnsCurrentPageWordsInFirstPage()
		{
			var word = "liepa";
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord(word) });

			var result = (ViewResult)_listPageController.Index();
			var model = (WordListModel)result.ViewData.Model;

			Assert.That(model.CurrentPageWords[0], Is.EqualTo(word));
		}

		[Test]
		public void Index_ReturnsCurrentPageWordsInSecondPage()
		{
			var page = 2;
			var wordB = "wordB";

			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord(wordB) });

			var result = (ViewResult)_listPageController.Index(page);
			var model = (WordListModel)result.ViewData.Model;

			Assert.That(model.CurrentPageWords[0], Is.EqualTo(wordB));
		}

		[Test]
		public void Index_ReturnsCorrectWordsCountInCurrentPage()
		{
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord("wordB"),
				new AnagramWord("wordC"), new AnagramWord("wordD") });

			var result = (ViewResult)_listPageController.Index(1);
			var model = (WordListModel)result.ViewData.Model;

			Assert.That(model.CurrentPageWords.Count, Is.EqualTo(_mockConfig.Object.TotalAmount));
		}

		[Test]
		public void Index_ReturnsCorrectCurrentPage_IfItIstSet()
		{
			var page = 5;
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("") });

			var result = (ViewResult)_listPageController.Index(page);
			var model = (WordListModel)result.ViewData.Model;

			Assert.That(model.CurrentPage, Is.EqualTo(page));
		}

		[Test]
		public void Index_ReturnsCorrectTotalPages()
		{			
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord("wordB"),
				new AnagramWord("wordC"), new AnagramWord("wordD") });

			var result = (ViewResult)_listPageController.Index(1);
			var model = (WordListModel)result.ViewData.Model;

			Assert.That(model.TotalPages, Is.EqualTo(4));
		}

		[Test]
		public void Index_ReturnsCorrectTotalWords()
		{
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord("wordB"),
				new AnagramWord("wordC"), new AnagramWord("wordD") });

			var result = (ViewResult)_listPageController.Index(1);
			var model = (WordListModel)result.ViewData.Model;

			Assert.That(model.TotalWords, Is.EqualTo(4));
		}
	}
}
