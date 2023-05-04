using BusinessLogic.AnagramActions;


namespace AnagramSolverTests.BusinessLogicTests.AnagramActionsTests
{
    public class DictionaryActionsTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TrimIfTooManyItems_ReturnsShortenedList_IfMaxAmountIsLessThanListCount()
        {
            List<string> list = new()
            {
                "aaa",
                "bbb",
                "ccc",
            };

            int maxAmount = 2;

            List<string> actualResult = list.TrimIfTooManyItems(maxAmount);
            List<string> expResult = list.Take(maxAmount).ToList();

            Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
        }

        [Test]
        public void TrimIfTooManyItems_ReturnsFullList_IfMaxAmountIsMoreThanListCount()
        {
            List<string> list = new()
            {
                "aaa",
                "bbb",
                "ccc",
            };

            int maxAmount = 9;

            List<string> actualResult = list.TrimIfTooManyItems(maxAmount);

            Assert.That(actualResult, Has.Count.EqualTo(list.Count));
        }

        [Test]
        public void TrimIfTooManyItems_ReturnsFullList_IfMaxAmountIsEqualToListCount()
        {
            List<string> list = new()
            {
                "aaa",
                "bbb",
                "ccc",
            };

            int maxAmount = 3;

            List<string> actualResult = list.TrimIfTooManyItems(maxAmount);
            List<string> expResult = list.Take(maxAmount).ToList();

            Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
        }

        [Test]
        public void TrimIfTooManyItems_ReturnsEmptyList_IfListIsEmpty()
        {
            List<string> list = new();

            int maxAmount = 9;

            List<string> actualResult = list.TrimIfTooManyItems(maxAmount);

            Assert.IsEmpty(actualResult);
        }
    }
}