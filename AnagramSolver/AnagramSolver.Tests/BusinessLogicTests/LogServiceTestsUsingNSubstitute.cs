using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using NSubstitute;
using Shouldly;

namespace AnagramSolver.Tests.EfDbFirstTests
{
    [TestFixture]
    public class LogServiceTestsUsingNSubstitute
    {
        private LogService _logService;
        private IWordLogRepository _wordLogRepository;
        private ISearchLogRepository _searchLogRepository;

        [SetUp]
        public void SetUp()
        {
            _wordLogRepository = Substitute.For<IWordLogRepository>();
            _searchLogRepository = Substitute.For<ISearchLogRepository>();

            _logService = new LogService(_searchLogRepository, _wordLogRepository);
        }

        [Test]
        public void GetLastSearchInfo_ReturnsCorrectDateTime()
        {
            DateTime expectedResult = DateTime.UtcNow;
            SearchLogDto model = new("5", expectedResult, "labas");

            _searchLogRepository.GetLastSearch().Returns(model);

            var result = _logService.GetLastSearchInfo();

            result.ShouldNotBeNull();
            result.TimeStamp.ShouldBe(expectedResult);

            _searchLogRepository.Received().GetLastSearch();
        }

    }
}