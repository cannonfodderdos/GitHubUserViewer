using Core.ApplicationServices;
using Core.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserViewer.Controllers;

namespace Test
{
    [TestClass]
    public class Controllers
    {
        private UserService _userService = new UserService(new TestGitHubService());

        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithRepos()
        {
            // Arrange
            string username = "robconery";

            // Act
            var user = await _userService.GetUser(username, true);

            // Assert
            Assert.AreEqual(username, user.Name);
            Assert.IsNotNull(user.Repos);
            Assert.IsTrue(user.Repos.Count > 0);
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
