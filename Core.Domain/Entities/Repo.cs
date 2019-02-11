using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class Repo
    {
        public string Name { get; set; }
        public string URL { get; set; }
        public int StargazerCount { get; set; }
        public int WatcherCount { get; set; }

        public Repo(string name, string url, int stargazerCount, int watcherCount)
        {
            Name = name;
            URL = url;
            StargazerCount = stargazerCount;
            WatcherCount = watcherCount;
        }
    }
}
