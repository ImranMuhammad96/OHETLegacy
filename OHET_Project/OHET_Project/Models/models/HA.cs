using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class HA
    {
        [Key]
        [ForeignKey("IDAttribute")]
        public Attribute Attribute { get; set; }
        public int? IDAttribute { get; set; }

        [Key]
        [ForeignKey("IDHero")]
        public Hero Hero { get; set; }
        public int? IDHero { get; set; }
    }
}