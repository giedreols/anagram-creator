namespace AnagramSolver.Tests.WebAppTests
{
    [TestFixture]
    internal class WordListControllerTests
    {
        //private WordListController _listPageController;
        //private Mock<IWordServer> _mockWordRepository;
        //private Mock<WebApp.ConfigReader> _mockConfig;

        //[SetUp]
        //public void SetUp()
        //{
        //    _mockWordRepository = new Mock<IWordServer>(MockBehavior.Strict);
        //    _mockConfig = new Mock<WebApp.ConfigReader>(MockBehavior.Strict);
        //    _mockConfig.SetupAllProperties();
        //    _mockConfig.Object.ConfigOptions.TotalAmount = 1;

        //    _listPageController = new WordListController(_mockWordRepository.Object, _mockConfig.Object);
        //}

        //[Test]
        //public void Search_ReturnsWordListModel()
        //{
        //    var word = "a";
        //    _mockWordRepository.Setup(p => p.GetMatchingWords(word, 1, 1)).Returns(new WordsPerPageDto(new List<string> { word }, 1, 1));

        //    var result = (ViewResult)_listPageController.Search(word);

        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result.Model, Is.InstanceOf<WordListViewModel>());
        //}

        //[Test]
        //public void Search_ReturnsMatchingWords_IfTheyExist()
        //{
        //    var word = "a";
        //    var expectedResult = new List<string>() { "aa", "kva", "la" };
        //    _mockWordRepository.Setup(p => p.GetMatchingWords(word, 1, 1)).Returns(new WordsPerPageDto(expectedResult, 1, 1));

        //    var result = (ViewResult)_listPageController.Search(word);
        //    var model = result.Model as WordListViewModel;

        //    Assert.That(model.CurrentPageWords, Is.EqualTo(expectedResult));
        //}

        //[Test]
        //public void Index_ReturnsWordListModel()
        //{
        //    _mockWordRepository.Setup(p => p.GetWordsByPage(1, 1)).Returns(new WordsPerPageDto(new List<string>(), 1, 1));

        //    var result = (ViewResult)_listPageController.Index();

        //    Assert.That(result, Is.Not.Null);
        //    Assert.That(result.Model, Is.InstanceOf<WordListViewModel>());
        //}

        //[Test]
        //public void Index_ReturnsCurrentPageWordsInFirstPage()
        //{
        //    var word = "liepa";
        //    _mockWordRepository.Setup(p => p.GetWordsByPage(1, 1)).Returns(new WordsPerPageDto(new List<string>() { word }, 1, 1));

        //    var result = (ViewResult)_listPageController.Index();
        //    var model = result.ViewData.Model as WordListViewModel;

        //    Assert.That(model?.CurrentPageWords[0], Is.EqualTo(word));
        //}

        //[Test]
        //public void DownloadFile_ReturnsFileContentResult()
        //{
        //    _mockWordRepository.Setup(p => p.GetFileWithWords()).Returns(Array.Empty<byte>());

        //    var result = _listPageController.DownloadFile();

        //    Assert.That(result, Is.TypeOf<FileContentResult>());
        //}

        //[Test]
        //public void DownloadFile_ReturnsFileTypeEqualsToTxt()
        //{
        //    _mockWordRepository.Setup(p => p.GetFileWithWords()).Returns(Array.Empty<byte>());

        //    var result = (FileContentResult)_listPageController.DownloadFile();

        //    Assert.That(result.ContentType, Is.EqualTo("text/plain"));
        //}

        //[Test]
        //public void DownloadFile_ReturnsFileContentTyleIsByteArray()
        //{
        //    _mockWordRepository.Setup(p => p.GetFileWithWords()).Returns(Array.Empty<byte>());

        //    var result = (FileContentResult)_listPageController.DownloadFile();

        //    Assert.That(result.FileContents, Is.TypeOf<byte[]>());
        //}
    }
}
