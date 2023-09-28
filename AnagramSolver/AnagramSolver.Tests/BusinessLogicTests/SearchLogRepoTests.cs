using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using AnagramSolver.EF.DbFirst;
using Moq;

namespace AnagramSolver.Tests.EfDbFirstTests
{
    public class SearchLogRepoTests
    {
        private Mock<AnagramSolverDataContext> _mockDataContext;
        private ISearchLogRepository _searchLogRepo;

        [SetUp]
        public void SetUp()
        {
            _mockDataContext = new Mock<AnagramSolverDataContext>();
            _searchLogRepo = new SearchLogRepo(_mockDataContext.Object);
        }

        // testuoti _searchLogRepo.Add ir testuoti, kad buvo iskviesta su ta data
        // daryti interface, kuris apwrapina

        //[Test]
        //public void AddLog()
        //{
        //    var expectedTimeStamp = DateTime.UtcNow;

        //    var item = new SearchLogDto("userIp", expectedTimeStamp, "word");

        //    _searchLogRepo.Add(item);

        //    _timeProvider.Setup(x => x.UtcNow).Returns(expectedTimeStamp);

        //    _logService.LogSearch("word", "ip");

        //    _mockSearchLogRepo.Verify(x => x.Add(It.Is<SearchLogDto>(x => x.TimeStamp == expectedTimeStamp)));
        //}
    }
}