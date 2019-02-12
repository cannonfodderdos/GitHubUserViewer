using AutoMapper;
using Core.ApplicationServices;
using Core.Common;
using Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Test.UnitTests.Mocks;
using UserViewer.Controllers.API;
using UserViewer.ViewModels;

namespace Test.UnitTests.Controllers.API
{
    /// <summary>
    /// Unit Tests for UserViewer.Controllers.UserController
    /// </summary>
    [TestClass]
    public class UserControllerTests
    {

        #region Tests

        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithRepos()
        {
            // Arrange
            var controller = ArrangeUserController();
            string username = "robconery";

            // Act
            IHttpActionResult actionResult = await controller.Get(username, true);
            var contentResult = actionResult as OkNegotiatedContentResult<UserViewModel>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.IsInstanceOfType(contentResult.Content, typeof(UserViewModel));
            Assert.IsTrue(contentResult.Content.Repos.Count > 0);
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithoutRepos()
        {
            // Arrange
            var controller = ArrangeUserController();
            string username = "robconery";

            // Act
            IHttpActionResult actionResult = await controller.Get(username, false);
            var contentResult = actionResult as OkNegotiatedContentResult<UserViewModel>;

            // Assert
            Assert.AreEqual(username, contentResult.Content.Name);
            Assert.IsNull(contentResult.Content.Repos);
        }

        [TestMethod]
        public async Task GetUser_NotFoundResult()
        {
            // Arrange
            var controller = ArrangeUserController();
            string username = "doesntexist";

            // Act
            IHttpActionResult actionResult = await controller.Get(username, false);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        #endregion

        #region Private Methods

        private UserController ArrangeUserController()
        {
            var mockGitHubService = MockBuilder.BuildIGitHubService();
            var mockMapper = MockBuilder.BuildIMapper();
            var mockLogger = new Mock<ILogger>();

            var gitHubService = mockGitHubService.Object;
            var mapper = mockMapper.Object;
            var logger = mockLogger.Object;

            UserService userService = new UserService(gitHubService);

            return new UserController(userService, mapper, logger);
        }

        #endregion
    }
}
