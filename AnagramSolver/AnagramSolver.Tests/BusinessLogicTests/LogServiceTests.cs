using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Moq;

namespace AnagramSolver.Tests.EfDbFirstTests
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



        //[Test]
        //public void AddLog()
        //{
        //    var expectedTimeStamp = DateTime.UtcNow;

        //    _timeProvider = new Mock<ITimeProvider>();


        //    _timeProvider.Setup(x => x.UtcNow).Returns(expectedTimeStamp);

        //    // Call the method that uses the timeProvider
        //    _logService.LogSearchAsync("word", "ip");

        //    // Verify that the method in your service called the timeProvider
        //    _timeProvider.Verify(x => x.UtcNow, Times.Once);

        //    // Debug the actual and expected timestamps for verification
        //    var actualTimestamp = _timeProvider.Object.UtcNow;
        //    Console.WriteLine($"Actual Timestamp: {actualTimestamp}");
        //    Console.WriteLine($"Expected Timestamp: {expectedTimeStamp}");

        //    // Verify that the log was added with the expected timestamp
        //    _mockSearchLogRepo.Verify(x => x.AddAsync(It.Is<SearchLogDto>(x => x.TimeStamp == expectedTimeStamp)), Times.Once);
        //}


        //[Test]
        //public void AddLog()
        //{
        //    _timeProvider = new Mock<ITimeProvider>();

        //    var expectedTimeStamp = DateTime.UtcNow;


        //    _timeProvider.Setup(x => x.UtcNow).Returns(expectedTimeStamp);

        //    _logService.LogSearch("word", "ip");

        //    x => x.Add(It.Is<SearchLogDto>(x => x.TimeStamp == (DateTime?)expectedTimeStamp));
        //    _mockSearchLogRepo.Verify(x => x.Add(It.Is<SearchLogDto>(x => x.TimeStamp == expectedTimeStamp)));
        //}




        //[Test]
        //public void GetLastSearchInfo_ReturnsCorrectDateTime()
        //{
        //    DateTime expectedResult = DateTime.UtcNow;
        //    SearchLogDto model = new("5", expectedResult, "labas");
        //    _mockSearchLogRepo.Setup(w => w.GetLastSearchAsync()).Returns(model);

        //    SearchLogDto result = _logService.GetLastSearchInfo();

        //    Assert.That(result.TimeStamp, Is.EqualTo(expectedResult));
        //}
    }
}