using AnagramSolver.BusinessLogic.Helpers;

namespace AnagramSolver.Tests.BusinessLogicTests
{
    public class InputWordValidatorTests
    {
        //[Test]
        //[TestCase(".lus")]
        //[TestCase("a'us")]
        //[TestCase("al?s")]
        //[TestCase("alu@")]
        //public void Validate_AddsInvalidityReasonInvalidCharacters_IfContainsInvalidChars(string inputWord)
        //{
        //    var result = inputWord.Validate(1, 2);

        //    Assert.That(result, Is.EqualTo(WordRejectionReasons.InvalidChars));
        //}

        //[Test]
        //[TestCase("alus")]
        //[TestCase("5alūs")]
        //[TestCase("alus-gardus")]
        //public void Validate_DoesNotAddInvalidityReasonInvalidCharacters_IfContainsValidChars(string inputWord)
        //{
        //    var result = inputWord.Validate(1, 20);

        //    Assert.That(result, Is.Null);
        //}

        //[Test]
        //public void Validate_AddsInvalidityReasonInvalidLength_IfTooShort()
        //{
        //    var result = "alus".Validate(5, 10);

        //    Assert.That(result, Is.EqualTo(WordRejectionReasons.TooShort));
        //}

        //[Test]
        //public void Validate_DoesNotAddInvalidityReasonInvalidLength_IfLengthIsEqualToMinLength()
        //{
        //    var result = "alus".Validate(4, 10);

        //    Assert.That(result, Is.Null);
        //}

        //[Test]
        //public void Validate_DoesNotAddInvalidityReasonInvalidLength_IfLengthIsBiggerThanMin()
        //{
        //    var result = "alus".Validate(3, 10);

        //    Assert.That(result, Is.Null);
        //}

        //[Test]
        //public void Validate_AddsInvalidityReasonInvalidLength_IfWordIsTooLong()
        //{
        //    var result = "alus".Validate(1, 3);

        //    Assert.That(result, Is.EqualTo(WordRejectionReasons.TooLong));
        //}

        //[Test]
        //public void Validate_DoesNotAddInvalidityReasonTooLOng_IfWordIsEqualToMaxLength()
        //{
        //    var result = "alus".Validate(1, 4);

        //    Assert.That(result, Is.Null);
        //}
    }
}