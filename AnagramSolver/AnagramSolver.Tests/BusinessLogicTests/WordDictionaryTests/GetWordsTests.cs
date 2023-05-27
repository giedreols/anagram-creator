using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.WordDictionaryTests
{
	public class GetWordsTests
	{
		private Mock<IFileReader> _mockFileReader;
		private IWordRepository _wordRepository;

		private string _text;

		[SetUp]
		public void Setup()
		{
			_mockFileReader = new Mock<IFileReader>(MockBehavior.Strict);
			_wordRepository = new WordDictionary(_mockFileReader.Object);

			_text = "";
		}

		[Test]
		public void GetWords_ThrowsError_IfFileIsEmpty()
		{
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			Assert.Throws<IndexOutOfRangeException>(() => _wordRepository.GetWords());
		}

		[Test]
		public void GetWords_ThrowsError_IfLinesInFileContainsLessThan3Fields()
		{
			_text = _text = "siela\tdkt.";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			Assert.Throws<IndexOutOfRangeException>(() => _wordRepository.GetWords());
		}

		[Test]
		public void GetWords_ReturnsImmutableList()
		{
			_text = "siela\tdkt.\tsiena";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			var result = _wordRepository.GetWords();

			Assert.That(result, Is.InstanceOf(typeof(List<AnagramWordModel>)));
		}

		[Test]
		public void GetWords_ReturnsOnlyUniqueWords()
		{
			_text = "siela\tdkt.\tsiela\r\nsiela\tdkt.\tsielas";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			IList<AnagramWordModel> result = _wordRepository.GetWords();

			Assert.That(result, Has.Count.EqualTo(2));
		}

		[Test]
		public void GetWords_ReturnsOnlyUniqueWordsCaseInsensitive()
		{
			_text = "jūra\tdkt.\tJūra\r\nJŪRA\tdkt.\tjūra";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			IList<AnagramWordModel> result = _wordRepository.GetWords();

			Assert.That(result, Has.Count.EqualTo(1));
		}


		[Test]
		public void GetWords_ReturnsMainFormSameAsInDictionary()
		{
			string expectedResult = "Jūra";
			_text = $"{expectedResult}\tdkt.\tJūra";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			IList<AnagramWordModel> result = _wordRepository.GetWords();

			Assert.That(result[0].MainForm, Is.EqualTo(expectedResult));
		}

		[Test]
		public void GetWords_ReturnsCorrectLowerCasedForm()
		{
			string word = "Jūra";
			string wordLowerCase = word.ToLower();
			_text = $"{word}\tdkt.\tJūra";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			IList<AnagramWordModel> result = _wordRepository.GetWords();

			Assert.That(result[0].LowerCaseForm, Is.EqualTo(wordLowerCase));
		}

		[Test]
		public void GetWords_ReturnsCorrectOrderedDescendingForm()
		{
			string word = "jūra";
			string sortedWord = new(word.OrderByDescending(c => c).ToArray());
			_text = $"{word}\tdkt.\tJūra";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			IList<AnagramWordModel> result = _wordRepository.GetWords();

			Assert.That(result[0].OrderedForm, Is.EqualTo(sortedWord));
		}

		[Test]
		public void GetWords_ReturnsWordListOrderedByLengthDescending()
		{
			string word1Char = "v";
			string word2Chars = "dd";
			string word3Chars = "aaa";
			string word4Chars = "4444";
			_text = $"{word3Chars}\tdkt.\t{word1Char}\r\n{word4Chars}\tdkt.\t{word2Chars}";
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			IList<AnagramWordModel> result = _wordRepository.GetWords();

			Assert.Multiple(() =>
			{
				Assert.That(result[0].MainForm, Is.EqualTo(word4Chars));
				Assert.That(result[1].MainForm, Is.EqualTo(word3Chars));
				Assert.That(result[2].MainForm, Is.EqualTo(word2Chars));
				Assert.That(result[3].MainForm, Is.EqualTo(word1Char));
			});
		}

		[TearDown]
		public void TearDown()
		{
			_text = "";
		}
	}
}