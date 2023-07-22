using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.Contracts.Interfaces;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.DictionaryFromFileTests
{
    public class GetWordsTests
    {
        private Mock<IFileReader> _mockFileReader;
        private IWordServer _wordRepository;

        private string _text;

        [SetUp]
        public void Setup()
        {
            _mockFileReader = new Mock<IFileReader>(MockBehavior.Strict);
            _wordRepository = new DictionaryFromFileActions(_mockFileReader.Object);

            _text = "";
        }

        [Test]
        public void GetWords_ThrowsError_IfFileIsEmpty()
        {
            _mockFileReader.Setup(p => p.ReadFile("zodynas.txt")).Returns(_text);

            Assert.Throws<IndexOutOfRangeException>(() => _wordRepository.GetWords());
        }

        [Test]
        public void GetWords_ThrowsError_IfLinesInFileContainsLessThan3Fields()
        {
            _text = _text = "siela\tdkt.";
            _mockFileReader.Setup(p => p.ReadFile("zodynas.txt")).Returns(_text);

            Assert.Throws<IndexOutOfRangeException>(() => _wordRepository.GetWords());
        }

        [TearDown]
        public void TearDown()
        {
            _text = "";
        }
    }
}