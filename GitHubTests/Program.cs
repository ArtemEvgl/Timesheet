using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubTests
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("my-cool"));

            var tokenAuth = new Credentials("8a54e8b7e4ca0604cbe3c1ac6cb0709e98b007e7"); // NOTE: not real token
            client.Credentials = tokenAuth;

            var user = await client.User.Get("pingvin1308");
            Console.WriteLine("{0} has {1} public repositories - go check out their profile at {2}",
                user.Name,
                user.PublicRepos,
                user.Url);

            var projects = await client
                .Repository
                .Project
                .GetAllForRepository("pingvin1308", "Timesheet");

            var timesheetProject = projects.FirstOrDefault();

            var columns = await client.Repository.Project.Column
                .GetAll(timesheetProject.Id);

            var cards = new List<ProjectCard>();

            foreach (var column in columns)
            {
                var columnCards = await client.Repository.Project.Card
                    .GetAll(column.Id);

                cards.AddRange(columnCards);
            }
        }
    }
}
