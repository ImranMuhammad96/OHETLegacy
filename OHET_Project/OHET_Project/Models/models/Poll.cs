using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Poll
    {
        [Key]
        public int IDPoll { get; set; }

        [ForeignKey("IDUser")]
        public User User { get; set; }
        public int? IDUser { get; set; }

        [Required]
        public string question { get; set; }

        public string description { get; set; }
           
        public ICollection<Answer> Answers { get; set; }
    }
}