using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Text;

namespace Git.Services
{
    public interface ICommitsService
    {
        void CreateCommit(string userId, string repositoryId, string description);

        CommitViewModel CreateViewModel(string repositoryId);

        ICollection<CommitAllViewModel> TakeOnlyMineCommits(string userId);

        void DeleteCommit(string commitId);

        bool IsOwnerOfCommit(string commitId, string userId);
    }
}
