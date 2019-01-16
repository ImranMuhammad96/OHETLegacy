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
    public class Rule
    {
        [Key]
        public int IDRule { get; set; }

        [Required]
        [StringLength(500)]
        public string title { get; set; }

        [Required]
        [StringLength(5000)]
        public string description { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }
    }
}