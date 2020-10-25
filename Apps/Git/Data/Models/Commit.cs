using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Git.Data.Models
{
    public class Commit
    {
        public Commit()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }


        [ForeignKey("Creator")]
        public string CreatorId { get; set; }

        public virtual User Creator { get; set; }


        [ForeignKey("Repository")]
        public string RepositoryId { get; set; }

        public virtual Repository Repository { get; set; }

    }
}
