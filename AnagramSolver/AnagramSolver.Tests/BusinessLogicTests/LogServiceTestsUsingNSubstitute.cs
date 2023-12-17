namespace AnagramSolver.Tests.BusinessLogicTests
{
    [TestFixture]
    public class LogServiceTestsUsingNSubstitute
    {
        //    private LogService _logService;
        //    private IWordLogRepository _wordLogRepository;
        //    private ISearchLogRepository _searchLogRepository;

        //    [SetUp]
        //    public void SetUp()
        //    {
        //        _wordLogRepository = Substitute.For<IWordLogRepository>();
        //        _searchLogRepository = Substitute.For<ISearchLogRepository>();

        //        _logService = new LogService(_searchLogRepository, _wordLogRepository);
        //    }

        //[Test]
        //public void GetLastSearchInfo_ReturnsCorrectDateTime()
        //{
        //    DateTime expectedResult = DateTime.UtcNow;
        //    SearchLogDto model = new("5", expectedResult, "labas");

        //    _searchLogRepository.GetLastSearchAsync().Returns(model);

        //    var result = _logService.GetLastSearchInfoAsync();

        //    result.ShouldNotBeNull();
        //    result.TimeStamp.ShouldBe(expectedResult);

        //    _searchLogRepository.Received().GetLastSearchAsync();
        //}

    }
}