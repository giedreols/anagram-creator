using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.Extensions.FileProviders;
using Moq;
using NSubstitute;
using NuGet.Packaging.Signing;

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

            //_logService = new LogService(_mockSearchLogRepo.Object, _mockWordLogRepo.Object, _mockTimeProvider.Object);
        }

        //[Test]
        //public async Task AddLog()
        //{
        //    var expectedTimeStamp = DateTime.UtcNow;
        //    _mockTimeProvider.Setup(p => p.UtcNow).Returns(expectedTimeStamp);

        //    var model = new SearchLogDto("55", expectedTimeStamp, "liepa");

        //    var result = Task.FromResult(1);

        //    _mockSearchLogRepo.Setup(x => x.AddAsync(model)).Returns(result);
            
        //    await _logService.LogSearchAsync("liepa", "55");

        //    _mockSearchLogRepo.Verify(x => x.AddAsync(It.Is<SearchLogDto>(x => x.TimeStamp == expectedTimeStamp)), Times.Once);
        //}
    }
}