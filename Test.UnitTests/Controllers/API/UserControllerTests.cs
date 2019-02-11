using Core.ApplicationServices;
using Core.Common;
using Core.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using UserViewer.Controllers.API;

namespace Test.UnitTests.Controllers.API
{
    /// <summary>
    /// Unit Tests for UserViewer.Controllers.UserController
    /// </summary>
    [TestClass]
    public class UserControllerTests
    {
        private UserService _userService = new UserService(new TestGitHubService());

        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithRepos()
        {
            // Arrange
            UserController controller = new UserController(_userService);
            string username = "robconery";

            // Act
            IHttpActionResult actionResult = await controller.Get(username, true);
            var contentResult = actionResult as OkNegotiatedContentResult<User>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithoutRepos()
        {
            // Arrange
            string username = "robconery";

            // Act
            var user = await _userService.GetUser(username, false);

            // Assert
            Assert.AreEqual(username, user.Name);
            Assert.IsNull(user.Repos);
        }

        [TestMethod]
        [ExpectedException(typeof(ApiException))]
        public async Task GetUser_404Exception()
        {
            // Arrange
            string username = "doesntexist";

            // Act
            try
            {
                var user = await _userService.GetUser(username, false);
            }
            catch (ApiException ex)
            {
                // Assert - double check ApiException is of status code 404
                Assert.AreEqual(404, ex.StatusCode);
                throw;
            }
        }
    }
}
