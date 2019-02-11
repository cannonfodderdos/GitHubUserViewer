﻿using AutoMapper;
using Core.ApplicationServices;
using Core.Common;
using Core.Domain.Entities;
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
        private UserService _userService = new UserService(new TestGitHubService());

        [TestMethod]
        public async Task GetUser_ShouldReturnUserWithRepos()
        {
            // Arrange
            var mapper = MockBuilder.BuildIMapper();
            UserController controller = new UserController(_userService, mapper.Object);
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
            var mapper = MockBuilder.BuildIMapper();
            UserController controller = new UserController(_userService, mapper.Object);
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
            var mapper = MockBuilder.BuildIMapper();
            UserController controller = new UserController(_userService, mapper.Object);
            string username = "doesntexist";

            // Act
            IHttpActionResult actionResult = await controller.Get(username, false);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }
    }
}
