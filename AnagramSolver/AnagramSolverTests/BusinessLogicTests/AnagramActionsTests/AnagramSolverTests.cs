using BusinessLogic.AnagramActions;
using BusinessLogic.DictionaryActions;
using Moq;

namespace AnagramSolverTests.BusinessLogicTests.AnagramActionsTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            // o kaip man uzzimokinti zodyna?????
            List<string> anagrams = new AnagramSolver().GetAnagrams("alus");

            List<string> expResult = new() { "sula" };

            Assert.That(anagrams, Is.EqualTo(expResult));
        }
    }
}