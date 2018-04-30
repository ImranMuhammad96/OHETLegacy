using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Attribute
    {
        [Key]
        public int IDAttribute { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public int level { get; set; }

        public ICollection<HA> HA { get; set; }
    }
}