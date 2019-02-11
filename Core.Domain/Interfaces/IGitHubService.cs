using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;

namespace Core.Domain.Interfaces
{
    public interface IGitHubService
    {
        Task<User> GetUser(string username);
        Task<ICollection<Repo>> GetRepos(string url);
    }
}
