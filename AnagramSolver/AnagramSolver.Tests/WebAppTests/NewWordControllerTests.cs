using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolver.Tests.WebAppTests
{
    [TestFixture]
    internal class NewWordControllerTests
    {
        private NewWordController _newWordController;
        private Mock<IWordRepository> _mockWordRepository;
        private Mock<WebApp.MyConfiguration> _mockConfig;
        private Mock<ILogHelper> _mockHelpers;

        [SetUp]
        public void SetUp()
        {
            _mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
            _mockConfig = new Mock<WebApp.MyConfiguration>();
            _mockHelpers = new Mock<ILogHelper>();
            _mockHelpers.Setup(m => m.LogSearch(It.IsAny<string>()))
                        .Callback<string>(inputWord =>
                        {
                            string ipAddress = "127.0.0.1";
                            DateTime now = DateTime.Now;
                        });
            _mockConfig.SetupAllProperties();
            _mockConfig.Object.ConfigOptions.TotalAmount = 10;
            _mockConfig.Object.ConfigOptions.MinLength = 1;
            _mockConfig.Object.ConfigOptions.MaxLength = 100;

            _newWordController = new NewWordController(_mockWordRepository.Object, _mockConfig.Object, _mockHelpers.Object);
        }

        // nevveikia.....

        [Test]
        public void Create_ReturnsNewWordModel_IfWordIsCreated()
        {
            FullWordDto word = new("lapas") { PartOfSpeechAbbreviation = "dkt." };
            WordWithAnagramsDto wordWithAnagrams = new WordWithAnagramsDto(word.OtherForm, new List<string>() { "palas" });

            // _mockWordRepository.Setup(p => p.SaveWord(It.Is<FullWordDto>(x => x.MainForm == "lapas" && x.PartOfSpeechAbbreviation == "dkt."),
            // _mockConfig.Object.ConfigOptions)).Returns(new NewWordDto() { IsSaved = true });

            _mockWordRepository.Setup(p => p.SaveWord(It.IsAny<FullWordDto>(),
                _mockConfig.Object.ConfigOptions)).Returns(new NewWordDto() { IsSaved = true });

            _mockWordRepository.Setup(p => p.GetAnagrams(word.MainForm)).Returns(wordWithAnagrams.Anagrams);

            var result = (ViewResult)_newWordController.Create(word.MainForm, word.PartOfSpeechAbbreviation);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());

            _mockWordRepository.Verify(p => p.SaveWord(It.Is<FullWordDto>(x => x.MainForm == "lapas" && x.PartOfSpeechAbbreviation == "dkt."), _mockConfig.Object.ConfigOptions));
        }

        [Test]
        public void Create_ReturnsAnagramsAfterWordsCreating_IfWordHasIt()
        {
            FullWordDto word = new("lapas") { PartOfSpeechAbbreviation = "dkt." };
            IList<string> anagrams = new List<string> { "aplas", "palas" };

            _mockWordRepository.Setup(p => p.SaveWord(It.Is<FullWordDto>(x => x.MainForm == "lapas" && x.PartOfSpeechAbbreviation == "dkt."),
            _mockConfig.Object.ConfigOptions)).Returns(new NewWordDto() { IsSaved = true });

            _mockWordRepository.Setup(p => p.GetAnagrams(word.MainForm)).Returns(new WordWithAnagramsDto(word.OtherForm, anagrams).Anagrams);

            var result = (ViewResult)_newWordController.Create(word.MainForm, word.PartOfSpeechAbbreviation);
            AnagramWordsModel model = result.Model as AnagramWordsModel;

            Assert.That(model.Anagrams, Is.EqualTo(anagrams));
        }

        [Test]
        public void Create_ReturnsEmptyListAfterWordsCreating_IfWordDoesNotHaveAnagrams()
        {
            FullWordDto word = new("zodis") { PartOfSpeechAbbreviation = "dkt." };

            _mockWordRepository.Setup(p => p.SaveWord(It.Is<FullWordDto>(x => x.MainForm == "zodis" && x.PartOfSpeechAbbreviation == "dkt."),
            _mockConfig.Object.ConfigOptions)).Returns(new NewWordDto() { IsSaved = true });

            _mockWordRepository.Setup(p => p.GetAnagrams(word.MainForm)).Returns(new WordWithAnagramsDto(word.OtherForm, new List<string>()).Anagrams);

            var result = (ViewResult)_newWordController.Create(word.MainForm, word.PartOfSpeechAbbreviation);
            AnagramWordsModel model = result.Model as AnagramWordsModel;

            Assert.That(model.Anagrams, Is.Empty);
        }


        [Test]
        public void Create_ReturnsViewNameEqualsToWordWithAnagrams_IfWordIsCreated()
        {
            FullWordDto word = new("zodis") { PartOfSpeechAbbreviation = "dkt." };

            _mockWordRepository.Setup(p => p.SaveWord(It.Is<FullWordDto>(x => x.MainForm == "zodis" && x.PartOfSpeechAbbreviation == "dkt."),
            _mockConfig.Object.ConfigOptions)).Returns(new NewWordDto() { IsSaved = true });

            _mockWordRepository.Setup(p => p.GetAnagrams(word.MainForm)).Returns(new WordWithAnagramsDto(word.OtherForm, new List<string>()).Anagrams);

            var result = (ViewResult)_newWordController.Create(word.MainForm, word.PartOfSpeechAbbreviation);

            Assert.That(result.ViewName, Is.EqualTo("../Home/WordWithAnagrams"));
        }
    }
}
