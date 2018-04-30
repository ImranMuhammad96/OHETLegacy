using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Other
    {
        [Key]
        [ForeignKey("IDItem")]
        public Item Item { get; set; }
        public int? IDItem { get; set; }
    }
}