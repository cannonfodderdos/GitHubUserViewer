using Core.Domain.Entities;
using Core.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.ApplicationServices
{
    public class UserService
    {
        private IGitHubService _gitHubService;

        public UserService(IGitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }

        public async Task<User> GetUser(string username, bool includeRepos = false)
        {
            var user = await _gitHubService.GetUser(username);

            if (includeRepos)
                user.Repos = await _gitHubService.GetRepos(user.RepoURL);

            return user;
        }

    }
}
