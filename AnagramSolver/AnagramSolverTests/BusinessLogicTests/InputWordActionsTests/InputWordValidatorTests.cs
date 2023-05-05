using BusinessLogic.AnagramActions;
using BusinessLogic.DictionaryActions;
using BusinessLogic.InputWordActions;
using Contracts.Models;
using Microsoft.VisualStudio.Web.CodeGeneration;
using Moq;

namespace AnagramSolverTests.BusinessLogicTests.InputWordActionsTests
{
    public class InputWordValidatorTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase(".lus")]
        [TestCase("a'us")]
        [TestCase("al?s")]
        [TestCase("alu@")]

        public void Validate_AddsInvalidityReasonInvalidCharacters_IfContainsInvalidChars(string inputWord)
        {
            InputWord word = new(inputWord);

            word.Validate(1);

            Assert.That(word.InvalidityReason, Is.EqualTo(WordRejectionReasons.InvalidCharacters));
        }

        [Test]
        [TestCase("alus")]
        [TestCase("alaus puta")]
        [TestCase("5alūs")]
        [TestCase("alus-gardus")]
        public void Validate_DoesNotAddInvalidityReasonInvalidCharacters_IfContainsValidChars(string inputWord)
        {
            InputWord word = new(inputWord);

            word.Validate(1);

            Assert.That(word.InvalidityReason, Is.Null);
        }


        [Test]
        public void Validate_AddsInvalidityReasonInvalidLength_IfTooShort()
        {
            InputWord word = new("alus");

            word.Validate(5);

            Assert.That(word.InvalidityReason, Is.EqualTo(WordRejectionReasons.TooShort));
        }

        [Test]
        public void Validate_DoesNotAddInvalidityReasonInvalidLength_IfLengthIsEqualToMinLength()
        {
            InputWord word = new("alus");

            word.Validate(4);

            Assert.That(word.InvalidityReason, Is.Null);
        }

        [Test]
        public void Validate_DoesNotAddInvalidityReasonInvalidLength_IfLengthIsBiggerThanMin()
        {
            InputWord word = new("alus");

            word.Validate(3);

            Assert.That(word.InvalidityReason, Is.Null);
        }

    }
}