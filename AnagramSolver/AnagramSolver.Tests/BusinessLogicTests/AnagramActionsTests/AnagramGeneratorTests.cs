using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
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
			IList<string> expectedResult = new List<string>() { "palei", "pelai" };

			_mockWordRepository.Setup(p => p.GetAnagrams(word)).Returns(new CachedAnagramDto(true, expectedResult));

			IList<string> anagrams = _anagramGenerator.GetAnagrams(word);

			Assert.That(anagrams, Is.EqualTo(expectedResult));
		}

		[Test]
		public void GetAnagrams_ReturnsEmptyList_IfAnagramsAreCachedAndAbsent()
		{
			string word = "liepa";

			_mockWordRepository.Setup(p => p.GetAnagrams(word)).Returns(new CachedAnagramDto(true, new List<string>()));

			IList<string> anagrams = _anagramGenerator.GetAnagrams(word);

			Assert.That(anagrams.IsNullOrEmpty);
		}

		[Test]
		public void GetAnagrams_ReturnsAnagrams_IfAnagramsAreNotCachedAndPresent()
		{
			string word = "liepa";

			IList<string> expectedResult = new List<string>() { "palei", "pelai" };

			List<WordWithFormsDto> list = new() {
				new WordWithFormsDto("siela"),
				new WordWithFormsDto("liepa"),
				new WordWithFormsDto("upė"),
				new WordWithFormsDto("pelai"),
				new WordWithFormsDto("liepa"),
				new WordWithFormsDto("palei") };

			_mockWordRepository = new Mock<IWordRepository>(MockBehavior.Default);
			_mockWordRepository.Setup(p => p.GetAnagrams(word)).Returns(new CachedAnagramDto(false, new List<string>()));
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
			_mockWordRepository.Setup(p => p.GetAnagrams(word)).Returns(new CachedAnagramDto(false, new List<string>()));
			_mockWordRepository.Setup(p => p.GetWords()).Returns(new List<WordWithFormsDto>());

			_anagramGenerator = new BusinessLogic.AnagramActions.AnagramGenerator(_mockWordRepository.Object);

			IList<string> anagrams = _anagramGenerator.GetAnagrams(word);

			Assert.That(anagrams.IsNullOrEmpty);
		}
	}
}