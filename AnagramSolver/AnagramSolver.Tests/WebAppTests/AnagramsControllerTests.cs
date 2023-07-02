using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolver.Tests.WebAppTests
{
	[TestFixture]
	internal class AnagramsControllerTests
	{
		private AnagramsController _anagramsController;
		private Mock<IAnagramGenerator> _mockAnagramSolver;
		private Mock<IHttpContextAccessor> _mockHttpContextAncessor;
		private Mock<IWordRepository> _mockWordRepository;
		private Mock<Helpers> _mockHelpers;


		[SetUp]
		public void SetUp()
		{
			_mockAnagramSolver = new Mock<IAnagramGenerator>(MockBehavior.Default);
			_mockHttpContextAncessor = new Mock<IHttpContextAccessor>(MockBehavior.Strict);
			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockHelpers = new Mock<Helpers>(MockBehavior.Strict);
			_anagramsController = new AnagramsController(_mockAnagramSolver.Object, _mockHelpers.Object);
		}

		[Test]
		public void Index_ReturnsAnagramWordsModel()
		{
			var word = "a";
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());

			var result = (ViewResult)_anagramsController.Get(word);

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Index_ReturnsAnagrams_IfInputWordHasIt()
		{
			var word = "liepa";
			var anagram = "palei";
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string> { anagram });

			var result = (ViewResult)_anagramsController.Get(word);
			var model = (AnagramWordsModel)result.Model;

			Assert.That(model.Anagrams[0], Is.EqualTo(anagram));
		}

		[Test]
		public void Index_ReturnsInputWord()
		{
			var word = "liepa";
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());

			var result = (ViewResult)_anagramsController.Get(word);
			var model = (AnagramWordsModel)result.Model;

			Assert.That(model.Word, Is.EqualTo(word));
		}
	}
}
