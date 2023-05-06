using BusinessLogic.DictionaryActions;

namespace AnagramSolverTests.BusinessLogicTests.DictionaryActionsTests
{
    public class FileReaderTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // ar testuot skaityma is failo kazkaip?

        [Test]
        public void FileReader_ConstructorDoesNotThrowError_IfFilePathIsCorrect()
        {
            static void constructorDelegate()
            {
                new FileReader();
            }

            Assert.DoesNotThrow(constructorDelegate);
        }
    }
}