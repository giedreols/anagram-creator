namespace AnagramSolver.Tests.WebAppTests
{
    [TestFixture]
    internal class AnagramsControllerTests
    {
        //private AnagramsController _anagramsController;
        //private Mock<ILogHelper> _mockHelpers;
        //private Mock<IwordServer> _wordRepository;

        //[SetUp]
        //public void SetUp()
        //{
        //	_mockHelpers = new Mock<ILogHelper>();
        //	_wordRepository = new Mock<IwordServer>();

        //	_mockHelpers.Setup(m => m.LogSearch(It.IsAny<string>()))
        //				.Callback<string>(inputWord =>
        //				{
        //					string ipAddress = "127.0.0.1";
        //					DateTime now = DateTime.Now;
        //				});

        //	_anagramsController = new AnagramsController(_wordRepository.Object, _mockHelpers.Object);
        //}

        //[Test]
        //public void Get_ReturnsAnagramWordsModel()
        //{
        //	var word = "a";
        //          _wordRepository.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());

        //	var result = (ViewResult)_anagramsController.Get(word);

        //	Assert.That(result, Is.Not.Null);
        //	Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
        //}

        //[Test]
        //public void Get_ReturnsAnagrams_IfInputWordHasIt()
        //{
        //	var word = "liepa";
        //	var anagram = "palei";
        //          _wordRepository.Setup(p => p.GetAnagrams(word)).Returns(new List<string> { anagram });

        //	var result = (ViewResult)_anagramsController.Get(word);
        //	var model = (AnagramWordsModel)result.Model;

        //	Assert.That(model.Anagrams[0], Is.EqualTo(anagram));
        //}

        //[Test]
        //public void Get_ReturnsInputWord()
        //{
        //	var word = "liepa";
        //          _wordRepository.Setup(p => p.GetAnagrams(word)).Returns(new List<string>());

        //	var result = (ViewResult)_anagramsController.Get(word);
        //	var model = (AnagramWordsModel)result.Model;

        //	Assert.That(model.Word, Is.EqualTo(word));
        //}
    }
}
