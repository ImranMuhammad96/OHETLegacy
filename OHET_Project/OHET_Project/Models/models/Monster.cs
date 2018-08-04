using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Monster
    {
        [Key]
        public int IDMonster { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        [ForeignKey("IDConcept")]
        public Concept Concept { get; set; }
        public int? IDConcept { get; set; }

        [Required]
        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }
    }
}