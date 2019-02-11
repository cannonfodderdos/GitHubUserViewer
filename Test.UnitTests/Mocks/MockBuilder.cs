using AutoMapper;
using Core.Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserViewer.ViewModels;

namespace Test.UnitTests.Mocks
{
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
    }
}
