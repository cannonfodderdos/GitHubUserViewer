using Core.ApplicationServices;
using Core.Common;
using Core.Domain.Interfaces;
using Core.Domain.Entities;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Test;

namespace UnitTests
{
    [TestClass]
    public class ApplicationServices
    {
        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithRepos()
        {
            // Arrange
            UserService service = new UserService(new TestGitHubService());
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
            UserService service = new UserService(new TestGitHubService());
            string username = "robconery";

            // Act
            var user = await service.GetUser(username, false);

            // Assert
            Assert.AreEqual(username, user.Name);
            Assert.IsNull(user.Repos);
        }
    }
}
