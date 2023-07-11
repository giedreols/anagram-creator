using AnagramSolver.Cli;

namespace AnagramSolver.Tests.CliTests
{
    public class AnagramValidatorTests
    {
        private List<string> _list;

        [SetUp]
        public void Setup()
        {
            _list = new()
            {
                "aaa",
                "bbb",
                "ccc",
            };
        }

        [Test]
        public void TrimIfTooManyItems_ReturnsShortenedList_IfMaxAmountIsLessThanListCount()
        {
            int maxAmount = 2;

            IList<string> expResult = _list.Take(maxAmount).ToList();

            IList<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

            Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
        }

        [Test]
        public void TrimIfTooManyItems_ReturnsFullList_IfMaxAmountIsMoreThanListCount()
        {
            int maxAmount = 9;

            IList<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

            Assert.That(actualResult, Has.Count.EqualTo(_list.Count));
        }

        [Test]
        public void TrimIfTooManyItems_ReturnsFullList_IfMaxAmountIsEqualToListCount()
        {
            int maxAmount = 3;

            IList<string> expResult = _list.Take(maxAmount).ToList();

            IList<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

            Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
        }

        [Test]
        public void TrimIfTooManyItems_ReturnsEmptyList_IfListIsEmpty()
        {
            _list.Clear();

            int maxAmount = 9;

            IList<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

            Assert.That(actualResult, Is.Empty);
        }
    }
}