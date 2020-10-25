using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;

        public RepositoriesController(IRepositoriesService repositoriesService)
        {
            this.repositoriesService = repositoriesService;
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        public HttpResponse All()
        {
            var allRepositories = this.repositoriesService.TakeAll();

            return this.View(allRepositories);
        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(name) || name.Length < 3 || name.Length> 10)
            {
                return this.Error("Repository name must be betwwen 3 and 10 symbols");
            }

            //TODO Enums here
            if (repositoryType.ToLower() != "public" && repositoryType.ToLower() != "private")
            {
                return this.Error("Repository type must be Public or Private!");
            }

            var userId = this.GetUserId();

            this.repositoriesService.Create(name, repositoryType, userId);

            return this.Redirect("/Repositories/All");
        }
    }
}
