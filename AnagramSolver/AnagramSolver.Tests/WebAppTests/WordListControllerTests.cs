﻿using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolver.Tests.WebAppTests
{
	[TestFixture]
	internal class WordListControllerTests
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
			_mockWordRepository.Setup(p => p.GetWordsByPage(1, 1)).Returns(new PageWordModel(new List<string>(), 1, 1) { });

			var result = (ViewResult)_listPageController.Index();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<WordListModel>());
		}

		[Test]
		public void Index_ReturnsCurrentPageWordsInFirstPage()
		{
			var word = "liepa";
			_mockWordRepository.Setup(p => p.GetWordsByPage(1, 1)).Returns(new PageWordModel(new List<string>() { word }, 1, 1) { });

			var result = (ViewResult)_listPageController.Index();
			var model = (WordListModel)result.ViewData.Model;

			Assert.That(model.CurrentPageWords[0], Is.EqualTo(word));
		}

		[Test]
		public void DownloadFile_ReturnsFileContentResult()
		{
			_mockWordRepository.Setup(p => p.GetFile()).Returns(Array.Empty<byte>());

			var result = _listPageController.DownloadFile();

			Assert.That(result, Is.TypeOf<FileContentResult>());
		}

		[Test]
		public void DownloadFile_ReturnsFileTypeEqualsToTxt()
		{
			_mockWordRepository.Setup(p => p.GetFile()).Returns(Array.Empty<byte>());

			var result = (FileContentResult)_listPageController.DownloadFile();

			Assert.That(result.ContentType, Is.EqualTo("text/plain"));
		}

		[Test]
		public void DownloadFile_ReturnsFileContentTyleIsByteArray()
		{
			_mockWordRepository.Setup(p => p.GetFile()).Returns(Array.Empty<byte>());

			var result = (FileContentResult)_listPageController.DownloadFile();

			Assert.That(result.FileContents, Is.TypeOf<byte[]>());
		}
	}
}