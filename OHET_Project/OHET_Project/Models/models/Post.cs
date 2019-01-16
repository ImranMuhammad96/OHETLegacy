using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    [DebuggerStepThrough]
    public class Post
    {
        public Post()
        {
            this.comments = new Collection<Comment>();
            this.subposts = new Collection<Subpost>();
        }

        [Key]
        public int IDPost { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public bool isPublic { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual string ApplicationUserId { get; set; }

        public ICollection<Comment> comments { get; set; }
        public ICollection<Subpost> subposts { get; set; }

    }
}