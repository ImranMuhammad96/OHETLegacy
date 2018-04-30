using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Equipment
    {
        [Key]
        public int IDEquipment { get; set; }

        [ForeignKey("IDHero")]
        public Hero Hero { get; set; }
        public int? IDHero { get; set; }

        public ICollection<EI> EI { get; set; }
    }
}