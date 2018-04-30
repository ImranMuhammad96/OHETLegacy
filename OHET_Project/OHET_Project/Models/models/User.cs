using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class User
    {
        [Key]
        public int IDUser { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string email { get; set; }

        public ICollection<Poll> Polls { get; set; }
        public ICollection<Answer> Answers { get; set; }
        public ICollection<Content> Contents { get; set; }
    }
}