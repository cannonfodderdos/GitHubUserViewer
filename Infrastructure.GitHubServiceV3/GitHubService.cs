using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Core.Common;
using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Infrastructure.GitHubServiceV3.Models;

namespace Infrastructure.GitHubServiceV3
{
    /// <summary>
    /// Service class that consumes GitHub's various V3 API's and returns expected domain objects
    /// </summary>
    public class GitHubService : IGitHubService
    {
        private HttpClient _client;
        
        public GitHubService()
        {
            // User-Agent required by GitHub API otherwise request is Forbidden (403)
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("User-Agent", "UserViewerTechTest");
        }

        /// <summary>
        /// Retrieve user details from GitHubs User API
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<User> GetUser(string username)
        {
            var response = await _client.GetAsync($"https://api.github.com/users/{username}");
            string responseBody = await response.Content.ReadAsStringAsync();

            // 200 response from User endpoint
            if (response.IsSuccessStatusCode)
            {
                // Take response string and convert to model based on Github documentation
                var gitHubUser = JsonConvert.DeserializeObject<GitHubUser>(responseBody);

                // Convert to domain entity and return
                return new User(
                    gitHubUser.Id,
                    gitHubUser.Avatar,
                    gitHubUser.Name == null ? gitHubUser.Login : gitHubUser.Name,
                    gitHubUser.Location,
                    gitHubUser.Repos_URL);
                
            }

            throw new ApiException((int)response.StatusCode, responseBody);
        }

        /// <summary>
        /// Retrieve collection of repos
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<List<Repo>> GetRepos(string url)
        {
            var response = await _client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Take response string and convert to model based on Github documentation
                var result = JsonConvert.DeserializeObject<IEnumerable<GitHubRepo>>(responseBody);

                // Convert to domain entity and return
                List<Repo> repos = new List<Repo>();

                foreach(var repo in result)
                {
                    repos.Add(new Repo(
                        repo.Name,
                        repo.URL,
                        repo.StargazersCount,
                        repo.WatchersCount));
                }

                return repos;
            }

            throw new ApiException((int)response.StatusCode, responseBody);
        }

    }
}
