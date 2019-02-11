using AutoMapper;
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
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Http.Results;
using Test.UnitTests.Mocks;
using UserViewer.Controllers.Web;
using UserViewer.ViewModels;

namespace Test.UnitTests.Controllers.Web
{
    /// <summary>
    /// Unit Tests for UserViewer.Controllers.UserController
    /// </summary>
    [TestClass]
    public class UserControllerTests
    {
        private UserService _userService = new UserService(new TestGitHubService());

        [TestMethod]
        public void UserControllerIndex()
        {
            // Arrange
            var mapper = MockBuilder.BuildIMapper();
            UserController controller = new UserController(_userService, mapper.Object);

            // Act
            var index = controller.Index();

            // Assert
            Assert.IsInstanceOfType(index, typeof(ViewResult));
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnPartialViewWithUserAndRepos()
        {
            // Arrange
            var mapper = MockBuilder.BuildIMapper();
            UserController controller = new UserController(_userService, mapper.Object);
            string username = "robconery";

            // Act
            PartialViewResult result = await controller.GetUser(username, true);
            UserViewModel model = result.Model as UserViewModel;

            // Assert
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Repos.Count > 0);
        }

        [TestMethod]
        public async Task GetUser_ShouldReturnPartialViewWithUserAndNoRepos()
        {
            // Arrange
            var mapper = MockBuilder.BuildIMapper();
            UserController controller = new UserController(_userService, mapper.Object);
            string username = "robconery";

            // Act
            PartialViewResult result = await controller.GetUser(username, false);
            UserViewModel model = result.Model as UserViewModel;

            // Assert
            Assert.IsNotNull(model);
            Assert.IsNull(model.Repos);
        }

        [TestMethod]
        public async Task GetUser_EmptyPartialViewAndViewBagError()
        {
            // Arrange
            var mapper = MockBuilder.BuildIMapper();
            UserController controller = new UserController(_userService, mapper.Object);
            string username = "doesntexist";

            // Act
            PartialViewResult result = await controller.GetUser(username, false);

            // Assert
            Assert.IsNull(result.Model);
            Assert.IsNotNull(result.ViewBag.Error);
        }
    }
}
