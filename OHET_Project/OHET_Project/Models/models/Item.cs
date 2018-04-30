using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Item
    {
        [Key]
        public int IDItem { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int cost { get; set; }

        [Required]
        public string description { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }

        public ICollection<EI> EI { get; set; }
    }
}