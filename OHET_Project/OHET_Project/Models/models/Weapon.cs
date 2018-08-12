using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Weapon : Item
    {
        public Weapon()
        {
            this.classes = new HashSet<Class>();
        }
        
        [Required]
        public string type { get; set; }

        public ICollection<Class> classes { get; set; }
    }
}