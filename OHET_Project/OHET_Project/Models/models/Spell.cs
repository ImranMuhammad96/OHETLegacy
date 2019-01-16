using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    [DebuggerStepThrough]
    public class Spell
    {
        [Key]
        public int IDSpell { get; set; }

        [Required]
        [StringLength(500)]
        public string name { get; set; }

        [Required]
        [StringLength(5000)]
        public string description { get; set; }

        [Required]
        [Range(7, 21)]
        public int conceptLvl { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }

        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public int? IDClass { get; set; }
    }
}