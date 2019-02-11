using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Domain.Entities
{
    public class User
    {
        private List<Repo> _repos;

        public int Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string RepoURL { get; set; }
        public List<Repo> Repos
        {
            get
            {
                if (_repos == null) return null;

                return _repos.OrderByDescending(x => x.StargazerCount).Take(5).ToList();
            }
            set
            {
                _repos = value;
            }
        }

        public User(int id, string avatar, string name, string location, string repoURL)
        {
            Id = id;
            Avatar = avatar;
            Name = name;
            Location = location;
            RepoURL = repoURL;
        }
    }
}
