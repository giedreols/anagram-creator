using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Contracts.Interfaces;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.WordDictionaryTests
{
	public class GetFileTests
	{
		private Mock<IFileReader> _mockFileReader;
		private WordDictionary _wordDictionary;

		[SetUp]
		public void Setup()
		{
			_mockFileReader = new Mock<IFileReader>(MockBehavior.Strict);
			_wordDictionary = new WordDictionary(_mockFileReader.Object);
		}

		[Test]
		public void GetFile_ReturnsByteArray()
		{
			var expectedResult = new byte[0];
			_mockFileReader.Setup(r => r.GetFile()).Returns(expectedResult);

			var actualResult = _wordDictionary.GetFileWithWords();

			Assert.That(actualResult, Is.TypeOf<byte[]>());
		}
	}
}