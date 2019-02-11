using Newtonsoft.Json;
using System;

namespace Infrastructure.GitHubServiceV3.Models
{
    public class GitHubUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("login")]
        public string Login { get; set; }
        [JsonProperty("avatar_url")]
        public string Avatar { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("followers")]
        public int Followers { get; set; }
        [JsonProperty("repos_url")]
        public string Repos_URL { get; set; }
    }
}
