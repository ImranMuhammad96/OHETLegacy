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
        /*
        public Equipment()
        {
            this.items = new HashSet<Item>();
        }
        */

        [Key]
        public int IDEquipment { get; set; }

        [Required]
        [ForeignKey("IDHero")]
        public Hero Hero { get; set; }
        public int? IDHero { get; set; }

        public virtual ICollection<Item> items { get; set; }
    }
}