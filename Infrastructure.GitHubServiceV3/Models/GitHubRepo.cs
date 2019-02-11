using Newtonsoft.Json;
using System;

namespace Infrastructure.GitHubServiceV3.Models
{
    public class GitHubRepo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("stargazers_count")]
        public int StargazersCount { get; set; }
        [JsonProperty("watchers_count")]
        public int WatchersCount { get; set; }
        [JsonProperty("url")]
        public string URL { get; set; }
    }
}
