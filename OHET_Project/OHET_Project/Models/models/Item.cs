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
    public class Item
    {
        [Key]
        public int IDItem { get; set; }

        [Required]
        [StringLength(500)]
        public string name { get; set; }

        [Required]
        public int conceptLvl { get; set; }

        [Required]
        [StringLength(5000)]
        public string description { get; set; }

        [Range(1, 500000)]
        public int cost { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }
    }
}