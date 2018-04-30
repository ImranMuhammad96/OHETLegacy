using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class EI
    {
        [Key]
        [ForeignKey("IDEquipment")]
        public Equipment Equipment { get; set; }
        public int? IDEquipment { get; set; }

        [Key]
        [ForeignKey("IDItem")]
        public Item Item { get; set; }
        public int? IDItem { get; set; }
    }
}