using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Contracts.Interfaces;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.WordDictionaryTests
{
	public class SaveWordTests
	{
		private Mock<IFileReader> _mockFileReader;
		private IWordRepository _wordRepository;

		private string _text;

		[SetUp]
		public void Setup()
		{
			_text =
				"siela\tdkt.\tsiela\r\n" +
				"pieva\tdkt.\tpievos\r\n" +
				"upė\tdkt.\tupės";

			_mockFileReader = new Mock<IFileReader>(MockBehavior.Strict);
			_mockFileReader.Setup(p => p.ReadFile()).Returns(_text);

			_wordRepository = new WordDictionary(_mockFileReader.Object);
		}

		[Test]
		public void SaveWord_ReturnTrue_IfWordIsUniqueAndSaved()
		{
			_mockFileReader.Setup(p => p.WriteFile("Adelė"));
			var result = _wordRepository.SaveWord("Adelė");
			Assert.IsTrue(result);
		}

		[Test]
		public void SaveWord_ReturnFalse_IfWordExistsInDictionary()
		{
			var result = _wordRepository.SaveWord("upė");
			Assert.IsTrue(result);
		}
	}
}