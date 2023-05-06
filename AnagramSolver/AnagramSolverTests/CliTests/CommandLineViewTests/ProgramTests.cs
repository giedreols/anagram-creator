using Cli;
using Moq;

namespace AnagramSolverTests.CliTests.CommandLineViewTests
{
    public class ProgramTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // kaip Program klase testuot? nu cia kazkoks integration testas gaunasi jau :o ar visa ta while loopa isskaidyt i metodus ir testuot atskirus metodus?

        [Test]
        public void Program__________()
        {
            var configMock = new Mock<Configuration>(MockBehavior.Strict);
            configMock.SetupGet(c => c.TotalAmount).Returns(1);
            configMock.SetupGet(c => c.MinLength).Returns(10);

        }
    }
}