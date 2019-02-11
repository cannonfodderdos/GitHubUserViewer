using Core.Common;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Test.UnitTests
{
    /// <summary>
    /// IGitHubService with test data
    /// </summary>
    public class TestGitHubService : IGitHubService
    {
        public async Task<User> GetUser(string username)
        {
            if (username == "robconery")
            {
                return new User(1, "testavatar.jpg", "robconery", "Florida", "https://api.github.com/users/robconery/repos");
            }
            else if (username == "helloworld")
            {
                return new User(2, "testavatar.jpg", "helloworld", "Sunderland", "https://api.github.com/users/helloworld/repos");
            }
            else
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Error finding user");
            }
        }

        public async Task<List<Repo>> GetRepos(string url)
        {
            switch (url)
            {
                case "https://api.github.com/users/robconery/repos":
                    {
                        return new List<Repo>
                                {
                                    new Repo("testrepo", "https://testrepo.com", 1, 0),
                                    new Repo("testrepo2", "https://testrepo2.com", 5, 20),
                                    new Repo("testrepo3", "https://testrepo3.com", 104, 1),
                                    new Repo("testrepo4", "https://testrepo4.com", 20, 5),
                                    new Repo("testrepo5", "https://testrepo5.com", 3, 1),
                                };
                    }
                default:
                    {
                        return new List<Repo>();
                    }
            }
        }
    }
}
