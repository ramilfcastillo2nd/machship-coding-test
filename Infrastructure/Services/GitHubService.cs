using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class GitHubService: IGitHubService
    {
        private readonly IConfiguration _config;
        public string Url { get; set; }
        public GitHubService(IConfiguration config)
        {
            _config = config;
            Url = _config["Github:Url"].ToString();
        }

        public async Task<List<GithubUserInfo>> GetUsersInfo(List<GithubUser> users)
        {
            var listUserInfos = new List<GithubUserInfo>();
            foreach (var user in users)
            {
                using (var client = new HttpClient())
                {
                    var url = $"{Url}/users/{user.login}";
                    client.DefaultRequestHeaders.Add("User-Agent", "request");
                    var response = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        var result = JsonConvert.DeserializeObject<GithubUserInfo>(content);
                        listUserInfos.Add(result);
                    }
                }
            }


            //Do Sorting
            

            return listUserInfos;
        }

        public async Task<List<GithubUser>> GetUsers()
        {
            using (var client = new HttpClient())
            {
                var url = $"{Url}/users";
                client.DefaultRequestHeaders.Add("User-Agent", "request");
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<List<GithubUser>>(content);
                    return result;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
