using System.Configuration;

namespace AnagramSolverTests.CliTests.CommandLineViewTests
{
    public class ConfigurationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        // nezinau ka ir kaip testuot :((((((

        // kaip ta errora uzsimokinti? ar yra prasme isvis ta testuot?

        [Test]
        public void Configuration_ThrowsError_IfValueNotExist()
        {
            static void constructorDelegate()
            {
                new Cli.Configuration();
            }

            Assert.Throws<ConfigurationErrorsException>(constructorDelegate);
        }

        [Test]
        public void Configuration_ThrowsError_IfValueNotInt()

        {
        }
    }
}