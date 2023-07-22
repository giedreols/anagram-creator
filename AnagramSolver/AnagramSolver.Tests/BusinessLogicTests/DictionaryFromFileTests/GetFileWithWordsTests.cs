using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.Contracts.Interfaces;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.DictionaryFromFileTests
{
    public class GetFileWithWordsTests
    {
        private Mock<IFileReader> _mockFileReader;
        private DictionaryFromFileActions _wordDictionary;

        [SetUp]
        public void Setup()
        {
            _mockFileReader = new Mock<IFileReader>(MockBehavior.Strict);
            _wordDictionary = new DictionaryFromFileActions(_mockFileReader.Object);
        }

        [Test]
        public void GetFileWithWords_ReturnsByteArray()
        {
            var expectedResult = new byte[0];
            _mockFileReader.Setup(r => r.GetFile()).Returns(expectedResult);

            var actualResult = _wordDictionary.GetFileWithWords();

            Assert.That(actualResult, Is.TypeOf<byte[]>());
        }
    }
}