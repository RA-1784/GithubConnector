namespace GithubConnector.Models
{
    public class Issue
    {

        public int Number { get; set; }
        public string ?Title { get; set; }
        public string ?State {  get; set; }
    }
}
