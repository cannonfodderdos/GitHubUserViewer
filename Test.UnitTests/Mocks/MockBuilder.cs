using AutoMapper;
using Core.Common;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UserViewer.ViewModels;

namespace Test.UnitTests.Mocks
{
    /// <summary>
    /// Class to include any logic to build complex mocking objects
    /// </summary>
    public static class MockBuilder
    {
        /// <summary>
        /// Mocks automapper for sake of unit test
        /// </summary>
        /// <returns></returns>
        public static Mock<IMapper> BuildIMapper()
        {
            var mock = new Mock<IMapper>();
            mock.Setup(x => x.Map<UserViewModel>(It.IsAny<User>()))
                .Returns((User user) =>
                {
                    var viewmodel = new UserViewModel()
                    {
                        Avatar = user.Avatar,
                        Location = user.Location,
                        Name = user.Name,
                    };

                    if (user.Repos != null)
                    {
                        var repos = new List<RepoViewModel>();

                        foreach(var repo in user.Repos)
                        {
                            repos.Add(new RepoViewModel()
                            {
                                Name = repo.Name,
                                StargazerCount = repo.StargazerCount,
                                URL = repo.URL
                            });
                        }

                        viewmodel.Repos = repos;
                    }

                    return viewmodel;
                });

            mock.Setup(x => x.Map<RepoViewModel>(It.IsAny<User>()))
                .Returns((Repo user) =>
                {
                    return new RepoViewModel()
                    {
                        Name = user.Name,
                        StargazerCount = user.StargazerCount,
                        URL = user.URL
                    };
                });

            return mock;
        }

        public static Mock<IGitHubService> BuildIGitHubService()
        {
            var mock = new Mock<IGitHubService>();

            // Setup GetUser
            mock.Setup(x => x.GetUser(It.IsAny<string>())).Throws(new ApiException((int)HttpStatusCode.NotFound, "Error finding user"));
            mock.Setup(x => x.GetUser(It.Is<string>(y => y.Equals("robconery")))).ReturnsAsync(() => new User(1, "testavatar.jpg", "robconery", "Florida", "https://api.github.com/users/robconery/repos"));

            // Setup GetRepos
            mock.Setup(x => x.GetRepos(It.Is<string>(y => y.Equals("https://api.github.com/users/robconery/repos")))).ReturnsAsync(
                () =>
                    new List<Repo>
                                    {
                                        new Repo("testrepo", "https://testrepo.com", 1, 0),
                                        new Repo("testrepo2", "https://testrepo2.com", 5, 20),
                                        new Repo("testrepo3", "https://testrepo3.com", 104, 1),
                                        new Repo("testrepo4", "https://testrepo4.com", 20, 5),
                                        new Repo("testrepo5", "https://testrepo5.com", 3, 1),
                                    }
                    );
            mock.Setup(x => x.GetUser(It.Is<string>(y => y.Equals("doesntexist")))).Throws(new ApiException((int)HttpStatusCode.NotFound, "Error finding user"));


            return mock;

        }
    }
}
