using BusinessLogic.AnagramActions;
using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Contracts.Models;
using Moq;

namespace AnagramSolverTests.BusinessLogicTests.AnagramActionsTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        private Mock<IWordRepository> mockWordRepository;

        [Test]
        public void Test1()
        {
            List<DictWord> list = new()
            {
                new DictWord("siela", "dkt.", "sielos"),
                new DictWord("rūkas", "dkt.", "rūkai"),
                new DictWord ("liepa", "dkt.", "liepoje"),
                new DictWord ("palei", "prl.", "palei"),
                new DictWord ("liesa", "bdv.", "liesos"),
                new DictWord ("sula", "dkt.", "sulos"),
            };

            mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
            mockWordRepository.Setup(p => p.GetWords()).Returns(list);

            // o kaip man uzzimokinti zodyna?????
            List<string> anagrams = new AnagramSolver(mockWordRepository.Object).GetAnagrams("uloss");

            List<string> expResult = new() { "sulos" };

            Assert.That(anagrams, Is.EqualTo(expResult));
        }
    }
}