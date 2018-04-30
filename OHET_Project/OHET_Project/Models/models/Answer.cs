using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Answer
    {
        [Key]
        public int IDAnswer { get; set; }

        [Required]
        public string choice { get; set; }

        public string comment { get; set; }

        [ForeignKey("IDPoll")]
        public Poll Poll { get; set; }
        public int? IDPoll { get; set; }

        [ForeignKey("IDUser")]
        public User User { get; set; }
        public int? IDUser { get; set; }
    }
}