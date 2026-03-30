using GithubConnector.Models;
using GithubConnector.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GithubConnector.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GitHubController : ControllerBase
    {
        private readonly IGitHubService _service;

        public GitHubController(IGitHubService service)
        {
            _service = service;
        }

        [HttpGet("repos/{username}")]
        public async Task<IActionResult> GetRepos(string username)
        {
                var repos=  await _service.GetRepositoriesAsync(username);
            return Ok(repos);
        }

        [HttpGet("issues")]

        public async Task<IActionResult> GetIssues(string owner,string repo)
        {

               var issues= await  _service.GetIssuesAsync(owner,repo);
            return Ok(issues);
        }

        [HttpPost("create-issue")]

        public async Task<IActionResult> CreateIssue([FromBody] CreateIssueRequest  request)
        {

               var result=   await _service.CreateIssueAsync(request);

            if (!result)
                return BadRequest("Issue creation failed");

            return Ok("Issue created successfully");
        }
    }
}
