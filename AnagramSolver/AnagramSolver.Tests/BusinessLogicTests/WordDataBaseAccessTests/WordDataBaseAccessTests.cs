using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests.WordDataBaseAccessTests
{
    public class WordDataBaseAccessTests
    {
        private WordDataBaseAccess _wordDbAccess;
        private Mock<IWordsActions> _mockWordTableActions;
        private Mock<ISearchLogActions> _mockSearchLogActions;

        [SetUp]
        public void Setup()
        {
            _mockWordTableActions = new Mock<IWordsActions>();
            _mockSearchLogActions = new Mock<ISearchLogActions>();
            _wordDbAccess = new WordDataBaseAccess(_mockWordTableActions.Object, _mockSearchLogActions.Object);
        }

        [Test]
        public void GetFileWithWords_ReturnsByteArray()
        {
            var expectedResult = new byte[0];
            _mockWordTableActions.Setup(r => r.GetWords()).Returns(new List<string>());

            var actualResult = _wordDbAccess.GetFileWithWords();

            Assert.That(actualResult, Is.TypeOf<byte[]>());
        }

        [Test]
        public void GetWordsByPage_ReturnsCurrentPageWordsInSecondPage()
        {
            var page = 2;
            var pageSize = 1;
            var wordB = "wordb";

            List<FullWordDto> allWords = new List<FullWordDto>
            {
                new FullWordDto("wordA"),
                new FullWordDto(wordB)
            };

            var expectedResult = new WordsPerPageDto(new List<string>() { wordB }, pageSize, allWords.Count());

            _mockWordTableActions.Setup(m => m.GetWords()).Returns(new List<string>
            {
                "wordA", wordB
            });

            WordsPerPageDto result = _wordDbAccess.GetWordsByPage(page, 1);

            Assert.That(result.Words[0], Is.EqualTo(expectedResult.Words[0]));
        }

        [Test]
        public void SaveWord_ReturnErrorMessage_IfWordIsTooShort()
        {
            FullWordDto word = new ("agurkas");
            ConfigOptionsDto config = new() { MinLength = 10 };

            NewWordDto result = _wordDbAccess.SaveWord(word, config);
            Assert.Multiple(() =>
            {
                Assert.That(result.IsSaved, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo(ErrorMessages.TooShort));
            });
        }

        [Test]
        public void SaveWord_ReturnErrorMessage_IfWordAlreadyExists()
        {
            FullWordDto word = new ("agurkas");
            ConfigOptionsDto config = new() { MinLength = 1, MaxLength = 50};

            _mockWordTableActions.Setup(m => m.IsWordExists(word.OtherForm)).Returns(true);

            NewWordDto result = _wordDbAccess.SaveWord(word, config);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSaved, Is.False);
                Assert.That(result.ErrorMessage, Is.EqualTo(ErrorMessages.AlreadyExists));
            });
        }

        [Test]
        public void SaveWord_DoesNotReturnErrorMessage_IfWordIsSaved()
        {
            FullWordDto word = new ("agurkas");
            ConfigOptionsDto config = new() { MinLength = 1, MaxLength = 50 };

            _mockWordTableActions.Setup(m => m.IsWordExists(word.OtherForm)).Returns(false);
            _mockWordTableActions.Setup(m => m.InsertWord(word)).Returns(true);

            NewWordDto result = _wordDbAccess.SaveWord(word, config);

            Assert.Multiple(() =>
            {
                Assert.That(result.IsSaved, Is.True);
                Assert.That(result.ErrorMessage, Is.Null);
            });
        }

        [Test]
        public void GetMatchingWords_ReturnsListOfMatchingWords()
        {
            string word = "lie";
            List<string> words = new() { "liepa", "liepai", "liepos", "liepas", "liepukai", "paliepimas", "paliek" };

            _mockWordTableActions.Setup(p => p.GetMatchingWords(word)).Returns(words);

            WordsPerPageDto result = _wordDbAccess.GetMatchingWords(word);

            Assert.That(result.Words, Has.Count.EqualTo(words.Count));
        }

        [Test]
        public void GetAnagrams_ReturnsListOfAnagrams_IfTheyExist()
        {
            string word = "liepa";
            List<string> words = new() { "pelai", "palei" };

            _mockWordTableActions.Setup(p => p.GetAnagrams(word)).Returns(words);

            var result = _wordDbAccess.GetAnagrams(word);

            Assert.That(result.Count(), Is.EqualTo(words.Count));
        }
    }
}