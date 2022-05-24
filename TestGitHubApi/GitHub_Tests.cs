using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.Json;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace TestGitHubApi
{
    public class GitHub_Tests
    {
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient("https://api.github.com");

            client.Authenticator = new HttpBasicAuthenticator("ivanova81", "token");

            string url = "/repos/ivanova81/postman/issues";

            this.request = new RestRequest(url);
        }

        [Test]
        public async Task Test_Get_Issue()
        {
            var response = await client.ExecuteAsync(request);

            var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

            foreach (var issue in issues)
            {
                Assert.IsNotNull(issue.html_url);
                Assert.IsNotNull(issue.id, "Issue id must not be null");
            }

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        private async Task<Issue> CreateIssue(string title, string body)
        {
            var request = new RestRequest("/repos/ivanova81/postman/issues");
            request.AddBody(new { body, title });
            var response = await this.client.ExecuteAsync(request, Method.Post);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);
            return issue;
        }

        [Test]
        public async Task Test_Create_GitHubIssueAsync()
        {
            string title = "Second Issue from RestSharp";
            string body = "Somebody here";

            var issue = await CreateIssue(title, body);

            Assert.Greater(issue.id, 0);
            Assert.Greater(issue.number, 0);
            Assert.IsNotEmpty(issue.title);
        }
    }
}