using Core.ApplicationServices;
using Core.Common;
using Core.Domain.Interfaces;
using Core.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Test.UnitTests.Mocks;

namespace Test.UnitTests.Core
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithRepos()
        {
            // Arrange
            var mockGitHubService = MockBuilder.BuildIGitHubService();
            var gitHubService = mockGitHubService.Object;
            UserService service = new UserService(gitHubService);
            string username = "robconery";

            // Act
            var user = await service.GetUser(username, true);

            // Assert
            Assert.AreEqual(username, user.Name);
            Assert.IsNotNull(user.Repos);
            Assert.IsTrue(user.Repos.Count > 0);
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithoutRepos()
        {
            // Arrange
            var mockGitHubService = MockBuilder.BuildIGitHubService();
            var gitHubService = mockGitHubService.Object;
            UserService service = new UserService(gitHubService);
            string username = "robconery";

            // Act
            var user = await service.GetUser(username, false);

            // Assert
            Assert.AreEqual(username, user.Name);
            Assert.IsNull(user.Repos);
        }
    }
}
