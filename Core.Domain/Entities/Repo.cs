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

        public Repo(string name, string url, int stargazerCount)
        {
            Name = name;
            URL = url;
            StargazerCount = stargazerCount;
        }
    }
}
