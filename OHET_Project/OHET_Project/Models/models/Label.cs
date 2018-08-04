using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Label
    {
        /*
        public Label()
        {
            this.heroes = new HashSet<Hero>();
        }
        */

        [Key]
        public int IDLabel { get; set; }

        [Required]
        public string name { get; set; }

        public string description { get; set; }

        [Required]
        public bool isActive { get; set; }

        public virtual ICollection<Hero> heroes { get; set; }
    }
}