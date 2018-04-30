using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class HL
    {
        [Key]
        [ForeignKey("IDHero")]
        public Hero Hero { get; set; }
        public int? IDHero { get; set; }

        [Key]
        [ForeignKey("IDLabel")]
        public Label Label { get; set; }
        public int? IDLabel { get; set; }
    }
}