using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Controllers;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AnagramSolver.Tests.WebAppTests
{
	[TestFixture]
	internal class UseInfoControllerTests
	{
		private UserInfoController _userInfoController;
		private Mock<IWordRepository> _mockDatabaseActions;
		private Mock<IAnagramGenerator> _mockAnagramGenerator;

		[SetUp]
		public void SetUp()
		{
			_mockDatabaseActions = new Mock<IWordRepository>(MockBehavior.Strict);
			_mockAnagramGenerator = new Mock<IAnagramGenerator>(MockBehavior.Strict);
			_userInfoController = new UserInfoController(_mockDatabaseActions.Object, _mockAnagramGenerator.Object);
		}

		[Test]
		public void Index_ReturnsUserInfoModel()
		{
			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(new SearchLogDto());
			_mockAnagramGenerator.Setup(g => g.GetAnagrams("")).Returns(new List<string>());

			var result = (ViewResult)_userInfoController.Index();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<UserInfoModel>());
		}

		[Test]
		public void Index_ReturnsLastSearchWord_IfItIsExists()
		{
			var word = "makaronas";
			SearchLogDto expectedResult = new()
			{
				Word = word,
				UserIp = "",
				TimeStamp = DateTime.Now,
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);
			_mockAnagramGenerator.Setup(g => g.GetAnagrams(word)).Returns(new List<string>());

			var result = (ViewResult)_userInfoController.Index();

			UserInfoModel model = result.Model as UserInfoModel;

			Assert.That(model.LastWord, Is.EqualTo(expectedResult.Word));
		}

		[Test]
		public void Index_ReturnsLastSearchDateTime_IfItIsExists()
		{
			var word = "makaronas";

			SearchLogDto expectedResult = new()
			{
				Word = word,
				UserIp = "",
				TimeStamp = DateTime.Now,
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);
			_mockAnagramGenerator.Setup(g => g.GetAnagrams(word)).Returns(new List<string>());

			var result = (ViewResult)_userInfoController.Index();

			UserInfoModel model = result.Model as UserInfoModel;

			Assert.That(model.SearchDateTime, Is.EqualTo(expectedResult.TimeStamp.ToString()));
		}

		[Test]
		public void Index_ReturnsAnagramsOfLastWord_IfTheyExist()
		{
			var word = "makaronas";

			List<string> expectedResult = new()
			{
				"marakonas"
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(new SearchLogDto()
			{
				Word = word,
				UserIp = "",
				TimeStamp = DateTime.Now,
			});
			_mockAnagramGenerator.Setup(g => g.GetAnagrams(word)).Returns(expectedResult);

			var result = (ViewResult)_userInfoController.Index();

			UserInfoModel model = result.Model as UserInfoModel;

			Assert.That(model.Anagrams[0], Is.EqualTo(expectedResult[0]));
		}

		[Test]
		public void Index_DoesNotThrowError_IfNoLastSearchInfo()
		{
			SearchLogDto expectedResult = new();
			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);

			var result = (ViewResult)_userInfoController.Index();

			Assert.DoesNotThrow(() =>
			{
				ViewResult result = (ViewResult)_userInfoController.Index();
			});

			UserInfoModel model = result.Model as UserInfoModel;
		}

		[Test]
		public void Index_DoesNotThrowError_IfNoAnagramsInLastSearch()
		{
			var word = "tu";
			SearchLogDto expectedResult = new()
			{
				Word = word,
				UserIp = "111",
				TimeStamp = DateTime.Now,
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);
			_mockAnagramGenerator.Setup(g => g.GetAnagrams(word)).Returns(new List<string>());

			Assert.DoesNotThrow(() =>
			{
				ViewResult result = (ViewResult)_userInfoController.Index();
			});
		}
	}
}
