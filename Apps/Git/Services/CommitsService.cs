using Git.Data;
using Git.Data.Models;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext db;

        public CommitsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool IsOwnerOfCommit(string commitId, string userId)
        {
            return this.db.Commits.Any(c => c.Id == commitId && c.CreatorId == userId);
        }

        public void CreateCommit(string userId, string repositoryId, string description)
        {
            var commit = new Commit()
            {
                Description = description,
                CreatedOn = DateTime.UtcNow,
                CreatorId = userId,
                RepositoryId = repositoryId
            };
            this.db.Commits.Add(commit);
            this.db.SaveChanges();
        }

        public CommitViewModel CreateViewModel(string repositoryId)
        {
            string repoName = this.db.Repositories.Where(x => x.Id == repositoryId).Select(x => x.Name).First();

            return new CommitViewModel()
            {
                Name = repoName,
                Id = repositoryId
            };
        }

        public void DeleteCommit(string commitId)
        {
            var currCommit = this.db.Commits.FirstOrDefault(c => c.Id == commitId);

            this.db.Remove(currCommit);
            this.db.SaveChanges();
        }

        public ICollection<CommitAllViewModel> TakeOnlyMineCommits(string userId)
        {
            return this.db.Commits.Where(c => c.CreatorId == userId)
                 .Select(x => new CommitAllViewModel
                 {
                     Name = x.Repository.Name,
                     Description = x.Description,
                     CreatedOn = x.CreatedOn.ToString("dd-MM-yyyy HH:mm"),
                     Id = x.Id
                 }).ToList();
        }
    }
}
