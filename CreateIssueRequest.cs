namespace GithubConnector.Models
{
    public class CreateIssueRequest
    {

        public string ?Owner { get; set; }
        public string ?Repo { get; set; }

        public string ?Title {  get; set; }
        public string ?Body { get; set; }
    }
}
