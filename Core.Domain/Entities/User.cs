using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string RepoURL { get; set; }
        public List<Repo> Repos { get; set; }

        public User(int id, string login, string avatar, string name, string repoURL)
        {
            Id = id;
            Login = login;
            Avatar = avatar;
            Name = name;
            RepoURL = repoURL;
        }
    }
}
