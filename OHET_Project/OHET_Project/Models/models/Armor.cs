using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Armor : Item
    {
        /*
        public Armor()
        {
            this.classes = new HashSet<Class>();
        }
        */

        [Required]
        public int dr { get; set; }

        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public int? IDClass { get; set; }

        public virtual ICollection<Class> classes { get; set; }
    }
}