using GithubConnector.Models;
namespace GithubConnector.Services
{
    public interface IGitHubService
    {
        Task<IEnumerable<Repo>> GetRepositoriesAsync(string username);
        Task<IEnumerable<Issue>>GetIssuesAsync(string owner, string repo);

        Task<bool> CreateIssueAsync(CreateIssueRequest request);
    }
}
