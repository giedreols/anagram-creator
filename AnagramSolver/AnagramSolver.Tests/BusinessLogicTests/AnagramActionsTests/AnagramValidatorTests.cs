using AnagramSolver.BusinessLogic.AnagramActions;

namespace AnagramSolver.Tests.BusinessLogicTests.AnagramActionsTests
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

			List<string> expResult = _list.Take(maxAmount).ToList();

			List<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

			Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
		}

		[Test]
		public void TrimIfTooManyItems_ReturnsFullList_IfMaxAmountIsMoreThanListCount()
		{
			int maxAmount = 9;

			List<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

			Assert.That(actualResult, Has.Count.EqualTo(_list.Count));
		}

		[Test]
		public void TrimIfTooManyItems_ReturnsFullList_IfMaxAmountIsEqualToListCount()
		{
			int maxAmount = 3;

			List<string> expResult = _list.Take(maxAmount).ToList();

			List<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

			Assert.That(actualResult, Has.Count.EqualTo(expResult.Count));
		}

		[Test]
		public void TrimIfTooManyItems_ReturnsEmptyList_IfListIsEmpty()
		{
			_list.Clear();

			int maxAmount = 9;

			List<string> actualResult = _list.TrimIfTooManyItems(maxAmount);

			Assert.That(actualResult, Is.Empty);
		}
	}
}