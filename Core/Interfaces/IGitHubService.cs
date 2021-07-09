using Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGitHubService
    {
        Task<List<GithubUser>> GetUsers();
        Task<GithubUserInfo> GetUserInfo(string userName);
    }
}
