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
    public class Ability
    {
        [Key]
        public int IDAbility { get; set; }

        [Required]
        [StringLength(5000)]
        public string description { get; set; }

        [Required]
        [Range(7, 15)]
        public int conceptLvl { get; set; }

        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public int? IDClass { get; set; }
    }
}