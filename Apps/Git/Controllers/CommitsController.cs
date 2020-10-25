using Git.Data;
using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;

        public CommitsController(ICommitsService commitsService)
        {
            this.commitsService = commitsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.commitsService.CreateViewModel(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string id, string description)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(description) || description.Length < 5)
            {
                return this.Error("Description should be at least 5 symbols!");
            }

            var userId = this.GetUserId();

            this.commitsService.CreateCommit(userId, id, description);//repositoryId

            return this.Redirect("/Repositories/All");
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            //Трябва да взема само моите commits

            var userId = this.GetUserId();

            var allMineCommits = this.commitsService.TakeOnlyMineCommits(userId);

            return this.View(allMineCommits);
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            //Провери дали User е същия

            var userId = this.GetUserId();

            if (!this.commitsService.IsOwnerOfCommit(id, userId))
            {
                return this.Error("You are not owner of that commit!");
            } 

            this.commitsService.DeleteCommit(id);

            return this.Redirect("/Commits/All");
        }
    }
}
