using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text.Json;

var client = new RestClient("https://api.github.com");

client.Authenticator = new HttpBasicAuthenticator("ivanova81", "token");

//var request = new RestRequest("/repos/ivanova81/postman/issues"); 

//var request = new RestRequest("/users/ivanova81/repos");

string url = "/repos/ivanova81/postman/issues";

var request = new RestRequest(url);

request.AddBody(new { title = "New Issue from RestSharp" });
    ;
var response = await client.ExecuteAsync(request, Method.Post);

Console.WriteLine("STATUS CODE : " + response.StatusCode);
Console.WriteLine("BODY : " + response.Content);

//var repos = JsonSerializer.Deserialize<List<Repo>>(response.Content);

//var issues = JsonSerializer.Deserialize<List<Issue>>(response.Content);

//foreach (var issue in issues)
//{
//    Console.WriteLine("ISSUE NUMBER : " + issue.number);

//    Console.WriteLine("ISSUE ID : " + issue.id);

//    Console.WriteLine("ISSUE URL : " + issue.html_url);

//    Console.WriteLine("*************");
//}

//foreach (var repo in repos)
//{
//    Console.WriteLine("REPO FULL NAME : " + repo.full_name);
//}

//Console.WriteLine("STATUS CODE : " + response.StatusCode);
//Console.WriteLine("BODY : " + response.Content);

