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
		private Mock<IAnagramGenerator> _mockAnagramSolver;
		private Mock<ControllerContext> _mockSession;

		[SetUp]
		public void SetUp()
		{
			_mockAnagramSolver = new Mock<IAnagramGenerator>(MockBehavior.Strict);
			_mockSession = new Mock<ControllerContext>(MockBehavior.Strict);

			//MockHttpSession httpcontext = new MockHttpSession();
			//httpcontext.Set<string>("UserEmail", "unittest@mycompany.com");


			//_mockSession.SetupAllProperties();
			//_mockSession.Object["key"] = "value";

		}

		[Test]
		public void Index_ReturnsUserInfoModel()
		{
			_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			var result = (ViewResult)_userInfoController.Index();

			Assert.That(result, Is.Not.Null);
			Assert.That(result.Model, Is.InstanceOf<UserInfoModel>());
		}

		[Test]
		public void Index_ReturnsLastSearchWord_IfItIsExists()
		{
			//_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			//var result = (ViewResult)_userInfoController.Index();

			//Assert.That(result, Is.Not.Null);
			//Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Index_ReturnsLastSearchDateTime_IfItIsExists()
		{
			//_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			//var result = (ViewResult)_userInfoController.Index();

			//Assert.That(result, Is.Not.Null);
			//Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Index_ReturnsAnagramsOfLastWord_IfTheyExist()
		{
			//_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			//var result = (ViewResult)_userInfoController.Index();

			//Assert.That(result, Is.Not.Null);
			//Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Index_DoesNotThrowError_IfNoLastSearchWord()
		{
			//_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			//var result = (ViewResult)_userInfoController.Index();

			//Assert.That(result, Is.Not.Null);
			//Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Index_DoesNotThrowError_IfNoLastSearchDateTime()
		{
			//_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			//var result = (ViewResult)_userInfoController.Index();

			//Assert.That(result, Is.Not.Null);
			//Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}

		[Test]
		public void Index_DoesNotThrowError_IfNoLastSearchAnagrams()
		{
			//_mockAnagramSolver.Setup(p => p.GetAnagrams("x")).Returns(new List<string>());

			//var result = (ViewResult)_userInfoController.Index();

			//Assert.That(result, Is.Not.Null);
			//Assert.That(result.Model, Is.InstanceOf<AnagramWordsModel>());
		}
	}
}
