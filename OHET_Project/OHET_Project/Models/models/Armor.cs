using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Armor
    {
        [Key]
        [ForeignKey("IDItem")]
        public Item Item { get; set; }
        public int? IDItem { get; set; }

        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public int? IDClass { get; set; }

        [Required]
        public int armorClass { get; set; }

        public ICollection<CA> CA { get; set; }
    }
}