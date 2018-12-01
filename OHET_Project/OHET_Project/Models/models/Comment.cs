using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Comment
    {
        [Key]
        public int IDComment { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual string ApplicationUserId { get; set; }

        [ForeignKey("IDPost")]
        public Post Post { get; set; }
        public int? IDPost { get; set; }
    }
}