using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Git.Data.Models
{
    public class User
    {
        public User()
        {
            this.Commits = new HashSet<Commit>();
            this.Repositories = new HashSet<Repository>();
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Repository> Repositories { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
    }
}
