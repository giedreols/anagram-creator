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

		[SetUp]
		public void SetUp()
		{
			_mockDatabaseActions = new Mock<IWordRepository>(MockBehavior.Strict);
			_userInfoController = new UserInfoController(_mockDatabaseActions.Object);
		}

		[Test]
		public void Index_ReturnsUserInfoModel()
		{
			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(new SearchLogDto());

			var result = (ViewResult)_userInfoController.Index();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<UserInfoModel>());
		}

		[Test]
		public void Index_ReturnsLastSearchWord_IfItIsExists()
		{
			SearchLogDto expectedResult = new()
			{
				Word = "makaronas",
				UserIp = "",
				TimeStamp = DateTime.Now,
				Anagrams = new List<string>()
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);

			var result = (ViewResult)_userInfoController.Index();

			UserInfoModel model = result.Model as UserInfoModel;

			Assert.That(model.LastWord, Is.EqualTo(expectedResult.Word));
		}

		[Test]
		public void Index_ReturnsLastSearchDateTime_IfItIsExists()
		{
			SearchLogDto expectedResult = new()
			{
				Word = "makaronas",
				UserIp = "",
				TimeStamp = DateTime.Now,
				Anagrams = new List<string>()
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);

			var result = (ViewResult)_userInfoController.Index();

			UserInfoModel model = result.Model as UserInfoModel;

			Assert.That(model.SearchDateTime, Is.EqualTo(expectedResult.TimeStamp.ToString()));
		}

		[Test]
		public void Index_ReturnsAnagramsOfLastWord_IfTheyExist()
		{
			SearchLogDto expectedResult = new()
			{
				Word = "",
				UserIp = "",
				TimeStamp = DateTime.Now,
				Anagrams = new List<string>() { "lanka" }
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);

			var result = (ViewResult)_userInfoController.Index();

			UserInfoModel model = result.Model as UserInfoModel;

			Assert.That(model.Anagrams[0], Is.EqualTo(expectedResult.Anagrams[0]));
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
			SearchLogDto expectedResult = new()
			{
				Word = "tu",
				UserIp = "111",
				TimeStamp = DateTime.Now,
				Anagrams = new List<string>()
			};

			_mockDatabaseActions.Setup(p => p.GetLastSearchInfo()).Returns(expectedResult);

			Assert.DoesNotThrow(() =>
			{
				ViewResult result = (ViewResult)_userInfoController.Index();
			});
		}
	}
}
