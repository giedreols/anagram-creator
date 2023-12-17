using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.EF.DbFirst.Entities;
using Moq;

namespace AnagramSolver.Tests.BusinessLogicTests
{
    public class SearchLogRepoTests
    {
        private Mock<AnagramSolverDataContext> _mockDataContext;
        private ISearchLogRepository _searchLogRepo;
        //private ITimeProvider _timeProvider;


        [SetUp]
        public void SetUp()
        {
            _mockDataContext = new Mock<AnagramSolverDataContext>();
            _searchLogRepo = new SearchLogRepo(_mockDataContext.Object);
        }

        // testuoti _searchLogRepo.Add ir testuoti, kad buvo iskviesta su ta data
        // daryti interface, kuris apwrapina

        [Test]
        public void AddLog()
        {
            // _timeProvider = new MockTimeProvider();

            //var expectedTimeStamp = _timeProvider.UtcNow;

            //var item = new SearchLogDto("userIp", expectedTimeStamp, "word");

            //    await _logService.LogSearchAsync("word", "ip");

            //    // Verify that the method in your service called the timeProvider
            //    _timeProvider.Verify(x => x.UtcNow, Times.Once);


            //    // Verify that the log was added with the expected timestamp
            //    _mockSearchLogRepo.Verify(x => x.AddAsync(It.Is<SearchLogDto>(x => x.TimeStamp == expectedTimeStamp)), Times.Once)

            //    x => x.Add(It.Is<SearchLogDto>(x => x.TimeStamp == (DateTime?)expectedTimeStamp));
            //    _mockSearchLogRepo.Verify(x => x.Add(It.Is<SearchLogDto>(x => x.TimeStamp == expectedTimeStamp)));





            //_searchLogRepo.Add(item);

            //_timeProvider.Setup(x => x.UtcNow).Returns(expectedTimeStamp);

            //_logService.LogSearch("word", "ip");

            //_mockSearchLogRepo.Verify(x => x.Add(It.Is<SearchLogDto>(x => x.TimeStamp == expectedTimeStamp)));
        }
    }
}