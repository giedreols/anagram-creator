using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.AnagramActionsTests
{
	public class AnagramSolverTests
	{
		private Mock<IWordRepository> _mockWordRepository;
		private IAnagramGenerator _anagramSolver;

		[SetUp]
		public void Setup()
		{
			List<AnagramWordModel> list = new() {
				new AnagramWordModel("siela"),
				new AnagramWordModel("alus"),
				new AnagramWordModel("upė"),
				new AnagramWordModel("liesa"),
				new AnagramWordModel("liepa"),
				new AnagramWordModel("sula") };

			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockWordRepository.Setup(p => p.GetWords()).Returns(list);

			_anagramSolver = new BusinessLogic.AnagramActions.AnagramGenerator(_mockWordRepository.Object);
		}


		[Test]
		public void GetAnagrams_ReturnsAnagram_IfInputWordHasIt()
		{
			List<string> expResult = new() { "liepa" };

			List<string> anagrams = _anagramSolver.GetAnagrams("palei");

			Assert.That(anagrams, Is.EqualTo(expResult));
		}

		[Test]
		public void GetAnagrams_ReturnsEmtpyList_IfInputWordDoesNotHaveIt()
		{
			List<string> anagrams = _anagramSolver.GetAnagrams("rytas");

			Assert.That(anagrams, Is.Empty);
		}

		[Test]
		public void GetAnagrams_ReturnsAnagram_IfInputWordInCapitals()
		{
			List<string> expResult = new() { "siela" };

			List<string> anagrams = _anagramSolver.GetAnagrams("LIESA");

			Assert.That(anagrams, Is.EqualTo(expResult));
		}

		[Test]
		public void GetAnagrams_ReturnsEmptyList_IfInputWordExistsInList()
		{
			List<string> anagrams = _anagramSolver.GetAnagrams("upė");

			Assert.That(anagrams, Is.Empty);
		}

	}
}