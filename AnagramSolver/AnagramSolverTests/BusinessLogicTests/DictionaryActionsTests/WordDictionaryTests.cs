using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Contracts.Models;
using Moq;
using System.Collections.Immutable;

namespace AnagramSolverTests.BusinessLogicTests.DictionaryActionsTests
{
    public class WordDictionaryTests
    {
        private Mock<IFileReader> mockFileReader;
        private IWordRepository wordRepository;

        [SetUp]
        public void Setup()
        {
            mockFileReader = new Mock<IFileReader>(MockBehavior.Strict);
            wordRepository = new WordDictionary(mockFileReader.Object);
        }

        [Test]
        public void GetWords_ReturnsEmptyList_IfFileIsEmpty()
        {
            IList<string> words = new List<string>()
            {
            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            var result = wordRepository.GetWords();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void GetWords_ReturnsImmutableList()
        {
            IList<string> words = new List<string>()
            {
            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            var result = wordRepository.GetWords();

            Assert.That(result, Is.InstanceOf(typeof(ImmutableList<AnagramWord>)));
        }

        [Test]
        public void GetWords_ReturnsOnlyUniqueWords()
        {
            IList<string> words = new List<string>()
            {
                "siela\tdkt.\tsiela",
                "siela\tdkt.\tsielas"
            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            IList<AnagramWord> result = wordRepository.GetWords();

            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public void GetWords_ReturnsOnlyUniqueWordsCaseInsensitive()
        {
            IList<string> words = new List<string>()
            {
                "jūra\tdkt.\tJūra",
                "JŪRA\tdkt.\tjūra"
            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            IList<AnagramWord> result = wordRepository.GetWords();

            Assert.That(result, Has.Count.EqualTo(1));
        }


        [Test]
        public void GetWords_ReturnsMainFormSameAsInDictionary()
        {
            string expectedResult = "Jūra";

            IList<string> words = new List<string>()
            {
                $"{expectedResult}\tdkt.\tJūra"
            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            IList<AnagramWord> result = wordRepository.GetWords();

            Assert.That(result[0].MainForm, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetWords_ReturnsCorrectLowerCasedForm()
        {
            string word = "Jūra";
            string wordLowerCase = word.ToLower();

            IList<string> words = new List<string>()
            {
                $"{word}\tdkt.\tJūra"
            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            IList<AnagramWord> result = wordRepository.GetWords();

            Assert.That(result[0].LowerCaseForm, Is.EqualTo(wordLowerCase));
        }

        [Test]
        public void GetWords_ReturnsCorrectOrderedDescendingForm()
        {
            string word = "jūra";
            string sortedWord = new(word.OrderByDescending(c => c).ToArray());

            IList<string> words = new List<string>()
            {
                $"{word}\tdkt.\tJūra"
            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            IList<AnagramWord> result = wordRepository.GetWords();

            Assert.That(result[0].OrderedForm, Is.EqualTo(sortedWord));
        }

        [Test]
        public void GetWords_ReturnsWordListOrderedByLengthDescending()
        {
            string word1Char = "v";
            string word2Chars = "dd";
            string word3Chars = "aaa";
            string word4Chars = "4444";

            IList<string> words = new List<string>()
            {
                $"{word3Chars}\tdkt.\t{word1Char}",
                $"{word4Chars}\tdkt.\t{word2Chars}",

            };

            mockFileReader.Setup(p => p.ReadFile()).Returns(words);

            IList<AnagramWord> result = wordRepository.GetWords();
            Assert.Multiple(() =>
            {
                Assert.That(result[0].MainForm, Is.EqualTo(word4Chars));
                Assert.That(result[1].MainForm, Is.EqualTo(word3Chars));
                Assert.That(result[2].MainForm, Is.EqualTo(word2Chars));
                Assert.That(result[3].MainForm, Is.EqualTo(word1Char));
            });
        }
    }
}