using GithubConnector.Configuration;
using GithubConnector.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace GithubConnector.Services
{
    public class GitHubService:IGitHubService
    {
        private readonly HttpClient _httpClient;
        private readonly GitHubSettings  _settings;

        public GitHubService(HttpClient httpClient,IOptions<GitHubSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;


            _httpClient.BaseAddress= new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization= new AuthenticationHeaderValue("Bearer",_settings.Token);
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("GitHubConnector", "1.0"));
        }

        public async Task<bool> CreateIssueAsync(CreateIssueRequest request)
        {

            var json = JsonSerializer.Serialize(new
            {
                title = request.Title,
                body = request.Body
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(
                $"/repos/{request.Owner}/{request.Repo}/issues", content);

            return response.IsSuccessStatusCode;

        }

        public async Task<IEnumerable<Issue>> GetIssuesAsync(string owner, string repo)
        {
          var response= await  _httpClient.GetAsync($"/repos/{owner}/{repo}/issues");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching in repositories");

            var content =  await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Issue>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
        }

        public async Task<IEnumerable<Repo>> GetRepositoriesAsync(string username)
        {
           var response= await _httpClient.GetAsync($"/users/{username}/repos");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error fetching repositories");


            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<Repo>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
        }
    }
}
