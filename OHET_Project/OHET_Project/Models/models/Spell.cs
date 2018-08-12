using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Spell
    {
        [Key]
        public int IDSpell { get; set; }

        [Required]
        public string description { get; set; }

        [ForeignKey("IDConcept")]
        public Concept Concept { get; set; }
        public int? IDConcept { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }

        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public int? IDClass { get; set; }
    }
}