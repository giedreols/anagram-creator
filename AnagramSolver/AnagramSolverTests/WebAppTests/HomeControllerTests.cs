using AnagramSolverWebApp.Controllers;
using AnagramSolverWebApp.Models;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolverTests.WebAppTests
{
	[TestFixture]
	internal class HomeControllerTests
	{
		private HomeController _homeController;
		private Mock<IAnagramSolver> _mockAnagramSolver;

		[SetUp]
		public void SetUp()
		{
			_mockAnagramSolver = new Mock<IAnagramSolver>(MockBehavior.Strict);
			_homeController = new HomeController(_mockAnagramSolver.Object);
		}

		[Test]
		public void Index_ReturnsAnagramWordsModel()
		{
			_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			var result = (ViewResult)_homeController.Index("");

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Index_ReturnsAnagrams_IfInputWordHasIt()
		{
			var word = "liepa";
			var anagram = "palei";
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string>() { anagram });

			var result = (ViewResult)_homeController.Index(word);
			var model = (AnagramWordsModel)result.Model;

			Assert.That(model.Anagrams[0], Is.EqualTo(anagram));
		}

		[Test]
		public void Index_ReturnsInputWord()
		{
			var word = "liepa";
			_mockAnagramSolver.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());

			var result = (ViewResult)_homeController.Index(word);
			var model = (AnagramWordsModel)result.Model;

			Assert.That(model.Word, Is.EqualTo(word));
		}
	}
}
