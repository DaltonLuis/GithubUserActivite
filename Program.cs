using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace GithubUserActivite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; AcmeInc/1.0)");

            Console.WriteLine("Enter a GitHub username:");
            var username = Console.ReadLine();

            if (!string.IsNullOrEmpty(username))
            {
                GetFromJsonAsync(httpClient, username).GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine("Invalid username.");
            }
        }

        static async Task GetFromJsonAsync(HttpClient httpClient, string username)
        {
            var events = await httpClient.GetFromJsonAsync<List<GitHubEvent>>(
                $"https://api.github.com/users/{username}/events");

            if (events != null)
            {
                Console.WriteLine("\nOutput:");
                foreach (var evt in events)
                {
                    var activity = FormatActivity(evt);
                    if (!string.IsNullOrEmpty(activity))
                    {
                        Console.WriteLine($"- {activity}");
                    }
                }
            }
            Console.WriteLine();
        }

        static string FormatActivity(GitHubEvent evt)
        {
            var repoName = evt.Repo?.Name ?? "unknown repository";
            
            return evt.Type switch
            {
                "PushEvent" => $"Pushed {evt.Payload?.Size ?? 0} commit(s) to {repoName}",
                "IssuesEvent" => $"Opened a new issue in {repoName}",
                "WatchEvent" => $"Starred {repoName}",
                "ForkEvent" => $"Forked {repoName}",
                "CreateEvent" => $"Created {evt.Payload?.RefType ?? "repository"} in {repoName}",
                "DeleteEvent" => $"Deleted {evt.Payload?.RefType ?? "branch"} in {repoName}",
                "PullRequestEvent" => $"{evt.Payload?.Action ?? "Opened"} a pull request in {repoName}",
                "PullRequestReviewEvent" => $"Reviewed a pull request in {repoName}",
                "IssueCommentEvent" => $"Commented on an issue in {repoName}",
                "PublicEvent" => $"Made {repoName} public",
                "MemberEvent" => $"Added a collaborator to {repoName}",
                _ => $"{evt.Type?.Replace("Event", "")} in {repoName}"
            };
        }
    }

    public class GitHubEvent
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("repo")]
        public GitHubRepo? Repo { get; set; }

        [JsonPropertyName("created_at")]
        public string? CreatedAt { get; set; }

        [JsonPropertyName("payload")]
        public GitHubPayload? Payload { get; set; }
    }

    public class GitHubPayload
    {
        [JsonPropertyName("ref_type")]
        public string? RefType { get; set; }

        [JsonPropertyName("action")]
        public string? Action { get; set; }

        [JsonPropertyName("size")]
        public int? Size { get; set; }
    }

    public class GitHubRepo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("url")]
        public string? Url { get; set; }
    }

}
