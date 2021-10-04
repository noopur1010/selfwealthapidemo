using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SelfWealthApiDemo
{

    public interface IGithubApiService
    {
        Task<User> Get(string userName);
    }

    public class GithubApiService : IGithubApiService
    {
        private HttpClient _httpClient;

        public GithubApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> Get(string userName)
        {
            User githubUser = null;
            string apiURL = $"/users/{userName}";
            var response = await _httpClient.GetAsync(apiURL);
            if (response.IsSuccessStatusCode)
            {
                string responeString = await response.Content.ReadAsStringAsync();
                githubUser = JsonSerializer.Deserialize<User>(responeString);
            }
            return Task.FromResult(githubUser).Result;
        }

    }
}
