using AnagramSolver.BusinessLogic.InputWordActions;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Tests.BusinessLogicTests
{
	public class InputWordValidatorTests
	{
		[Test]
		[TestCase(".lus")]
		[TestCase("a'us")]
		[TestCase("al?s")]
		[TestCase("alu@")]
		public void Validate_AddsInvalidityReasonInvalidCharacters_IfContainsInvalidChars(string inputWord)
		{
			InputWordModel word = new(inputWord);

			word.Validate(1);

			Assert.That(word.InvalidityReason, Is.EqualTo(WordRejectionReasons.InvalidChars));
		}

		[Test]
		[TestCase("alus")]
		[TestCase("alaus puta")]
		[TestCase("5alūs")]
		[TestCase("alus-gardus")]
		public void Validate_DoesNotAddInvalidityReasonInvalidCharacters_IfContainsValidChars(string inputWord)
		{
			InputWordModel word = new(inputWord);

			word.Validate(1);

			Assert.That(word.InvalidityReason, Is.Null);
		}

		[Test]
		public void Validate_AddsInvalidityReasonInvalidLength_IfTooShort()
		{
			InputWordModel word = new("alus");

			word.Validate(5);

			Assert.That(word.InvalidityReason, Is.EqualTo(WordRejectionReasons.TooShort));
		}

		[Test]
		public void Validate_DoesNotAddInvalidityReasonInvalidLength_IfLengthIsEqualToMinLength()
		{
			InputWordModel word = new("alus");

			word.Validate(4);

			Assert.That(word.InvalidityReason, Is.Null);
		}

		[Test]
		public void Validate_DoesNotAddInvalidityReasonInvalidLength_IfLengthIsBiggerThanMin()
		{
			InputWordModel word = new("alus");

			word.Validate(3);

			Assert.That(word.InvalidityReason, Is.Null);
		}
	}
}