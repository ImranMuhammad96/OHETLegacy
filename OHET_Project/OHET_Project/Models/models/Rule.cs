using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Rule
    {
        [Key]
        public int IDRule { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }
    }
}