using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Subpost
    {
        [Key]
        public int IDSubpost { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [StringLength(5000)]
        public string Description { get; set; }

        [Required]
        public int OrderNr { get; set; }

        [ForeignKey("IDPost")]
        public Post Post { get; set; }
        public int? IDPost { get; set; }
    }
}