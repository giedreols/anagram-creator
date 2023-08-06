using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests
{
    public class LogServiceTests
    {
        private Mock<ISearchLogRepository> _mockSearchLogRepo;
        private Mock<IWordLogRepository> _mockWordLogRepo;
        private LogService _logService;

        [SetUp]
        public void SetUp()
        {
            _mockWordLogRepo = new Mock<IWordLogRepository>(MockBehavior.Default);
            _mockSearchLogRepo = new Mock<ISearchLogRepository>(MockBehavior.Default);

            _logService = new LogService(_mockSearchLogRepo.Object, _mockWordLogRepo.Object);
        }

        // MAN VEIKIA TIESIOG TAIP.... KAS CIA NEGERAI?

        [Test]
        public void GetLastSearchInfo_ReturnsCorrectDateTime()
        {
            DateTime expectedResult = DateTime.UtcNow;
            SearchLogDto model = new("5", expectedResult, "labas");
            _mockSearchLogRepo.Setup(w => w.GetLastSearch()).Returns(model);

            SearchLogDto result = _logService.GetLastSearchInfo();

            Assert.That(result.TimeStamp, Is.EqualTo(expectedResult));
        }
    }
}