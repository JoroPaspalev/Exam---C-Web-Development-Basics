using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext db;

        public RepositoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string repositoryType, string userId)
        {
            var isPublic = repositoryType.ToLower() == "public" ? true : false;

            var repository = new Repository
            {
                Name = name,
                CreatedOn = DateTime.UtcNow,
                IsPublic = isPublic,
                OwnerId = userId
            };

            this.db.Repositories.Add(repository);
            this.db.SaveChanges();
        }

        public ICollection<RepositoryViewModel> TakeAll()
        {
            //вземи всички Repositories които са Public

            return this.db.Repositories
                .Where(x => x.IsPublic == true)
                .Select(x => new RepositoryViewModel
                {
                    Id =x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    CreatedOn = x.CreatedOn.ToString("dd/MM/yyyy HH:mm"),
                    CommitsCount = x.Commits.Count
                }).ToList();
        }
    }
}
