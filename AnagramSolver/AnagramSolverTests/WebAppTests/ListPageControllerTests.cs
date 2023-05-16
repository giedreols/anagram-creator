using AnagamSolverWebApp.Controllers;
using AnagramSolverWebApp.Models;
using Contracts.Interfaces;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Immutable;

namespace AnagramSolverTests.WebAppTests
{
	[TestFixture]
	internal class ListPageControllerTests
	{
		private ListPageController _listPageController;
		private Mock<IWordRepository> _mockWordRepository;

		[SetUp]
		public void SetUp()
		{
			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_listPageController = new ListPageController(_mockWordRepository.Object);
		}

		[Test]
		public void Index_ReturnsListPagesModel()
		{
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("") }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<ListPageModel>());
		}

		[Test]
		public void Index_ReturnsCurrentPageWordsInFirstPage()
		{
			var word = "liepa";
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord(word) }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index();
			var model = (ListPageModel)result.ViewData.Model;

			Assert.That(model.CurrentPageWords[0], Is.EqualTo(word));
		}

		[Test]
		public void Index_ReturnsCurrentPageWordsInSecondPage()
		{
			var page = 2;
			var pageSize = 1;
			var wordB = "wordB";

			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord(wordB) }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index(page, pageSize);
			var model = (ListPageModel)result.ViewData.Model;

			Assert.That(model.CurrentPageWords[0], Is.EqualTo(wordB));			
		}

		[Test]
		public void Index_ReturnsCorrectWordsCountInCurrentPage()
		{
			var pageSixe = 2;

			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord("wordB"),
				new AnagramWord("wordC"), new AnagramWord("wordD") }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index(1, pageSixe);
			var model = (ListPageModel)result.ViewData.Model;

			Assert.That(model.CurrentPageWords.Count, Is.EqualTo(pageSixe));
		}

		[Test]
		public void Index_ReturnsCorrectCurrentPage_IfItIstSet()
		{
			var page = 5;
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("") }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index(page);
			var model = (ListPageModel)result.ViewData.Model;

			Assert.That(model.CurrentPage, Is.EqualTo(page));
		}

		[Test]
		public void Index_ReturnsCorrectPageSize_IfItIstSet()
		{
			var pageSize = 5;
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("") }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index(1, pageSize);
			var model = (ListPageModel)result.ViewData.Model;

			Assert.That(model.PageSize, Is.EqualTo(pageSize));
		}

		[Test]
		public void Index_ReturnsCorrectTotalPages()
		{
			var pageSize = 1;
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord("wordB"),
				new AnagramWord("wordC"), new AnagramWord("wordD") }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index(1, pageSize);
			var model = (ListPageModel)result.ViewData.Model;

			Assert.That(model.TotalPages, Is.EqualTo(4));
		}

		[Test]
		public void Index_ReturnsCorrectTotalWords()
		{
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<AnagramWord>() { new AnagramWord("wordA"), new AnagramWord("wordB"),
				new AnagramWord("wordC"), new AnagramWord("wordD") }.ToImmutableList());

			var result = (ViewResult)_listPageController.Index(1, 1);
			var model = (ListPageModel)result.ViewData.Model;

			Assert.That(model.TotalWords, Is.EqualTo(4));
		}
	}
}
