using BusinessLogic.AnagramActions;
using BusinessLogic.DictionaryActions;


namespace AnagramSolverTests.BusinessLogicTests.AnagramActionsTests
{
    public class DictionaryActionsTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void ValidateAmount_ReturnsShortenedList_IfMaxAmountIsLessThanListCount()
        {
            List<string> list = new()
            {
                "aaa",
                "bbb",
                "ccc",
            };

            int maxAmount = 2;

            List<string> actualResult = list.ValidateAmount(maxAmount);
            List<string> expResult = list.Take(maxAmount).ToList();

            Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
        }

        [Test]
        public void ValidateAmount_ReturnsFullList_IfMaxAmountIsMoreThanListCount()
        {
            List<string> list = new()
            {
                "aaa",
                "bbb",
                "ccc",
            };

            int maxAmount = 9;

            List<string> actualResult = list.ValidateAmount(maxAmount);

            Assert.That(actualResult, Has.Count.EqualTo(list.Count));
        }

        [Test]
        public void ValidateAmount_ReturnsFullList_IfMaxAmountIsEqualToListCount()
        {
            List<string> list = new()
            {
                "aaa",
                "bbb",
                "ccc",
            };

            int maxAmount = 3;

            List<string> actualResult = list.ValidateAmount(maxAmount);
            List<string> expResult = list.Take(maxAmount).ToList();

            Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
        }

        [Test]
        public void ValidateAmount_ReturnsEmptyList_IfListIsEmpty()
        {
            List<string> list = new();

            int maxAmount = 9;

            List<string> actualResult = list.ValidateAmount(maxAmount);

            Assert.IsEmpty(actualResult);
        }
    }
}