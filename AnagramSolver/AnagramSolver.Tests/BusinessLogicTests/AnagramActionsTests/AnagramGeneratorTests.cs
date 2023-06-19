using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.AnagramActionsTests
{
	public class AnagramGeneratorTests
	{
		private Mock<IWordRepository> _mockWordRepository;
		private IAnagramGenerator _anagramGenerator;

		[SetUp]
		public void Setup()
		{
			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
			_anagramGenerator = new BusinessLogic.AnagramActions.AnagramGenerator(_mockWordRepository.Object);
		}


		[Test]
		public void GetAnagrams_ReturnsAnagrams_IfAnagramsAreCachedAndPresent()
		{
			string word = "liepa";
			IList<string> expectedResult = new List<string>() {"palei", "pelai"};

			_mockWordRepository.Setup(p => p.GetCachedAnagrams(word)).Returns(new CachedAnagramModel(true, expectedResult));

			IList<string> anagrams = _anagramGenerator.GetAnagrams(word);

			Assert.That(anagrams, Is.EqualTo(expectedResult));
		}

		[Test]
		public void GetAnagrams_ReturnsEmptyList_IfAnagramsAreCachedAndAbsent()
		{
			string word = "liepa";

			_mockWordRepository.Setup(p => p.GetCachedAnagrams(word)).Returns(new CachedAnagramModel(true, new List<string>()));

			IList<string> anagrams = _anagramGenerator.GetAnagrams(word);

			Assert.That(anagrams.IsNullOrEmpty);
		}

		[Test]
		public void GetAnagrams_ReturnsAnagrams_IfAnagramsAreNotCachedAndPresent()
		{
			string word = "liepa";

			IList<string> expectedResult = new List<string>() { "palei", "pelai" };

			List<WordWithFormsModel> list = new() {
				new WordWithFormsModel("siela"),
				new WordWithFormsModel("liepa"),
				new WordWithFormsModel("upė"),
				new WordWithFormsModel("pelai"),
				new WordWithFormsModel("liepa"),
				new WordWithFormsModel("palei") };

			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Default);
			_mockWordRepository.Setup(p => p.GetCachedAnagrams(word)).Returns(new CachedAnagramModel(false, new List<string>()));
			_mockWordRepository.Setup(p => p.GetWords()).Returns(list);

			_anagramGenerator = new BusinessLogic.AnagramActions.AnagramGenerator(_mockWordRepository.Object);

			IList<string> anagrams = _anagramGenerator.GetAnagrams(word);

			Assert.That(anagrams, Is.EqualTo(expectedResult));
		}

		[Test]
		public void GetAnagrams_ReturnsEmptyList_IfAnagramsAreNotCachedAndAbsent()
		{
			string word = "pieva";

			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Default);
			_mockWordRepository.Setup(p => p.GetCachedAnagrams(word)).Returns(new CachedAnagramModel(false, new List<string>()));
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<WordWithFormsModel>());

			_anagramGenerator = new BusinessLogic.AnagramActions.AnagramGenerator(_mockWordRepository.Object);

			IList<string> anagrams = _anagramGenerator.GetAnagrams(word);

			Assert.That(anagrams.IsNullOrEmpty);
		}
	}
}