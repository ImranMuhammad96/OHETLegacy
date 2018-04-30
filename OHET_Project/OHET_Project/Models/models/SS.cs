using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class SS
    {
        [Key]
        [ForeignKey("IDSpell")]
        public Spell Spell { get; set; }
        public int? IDSpell { get; set; }

        [Key]
        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public int? IDClass { get; set; }
    }
}