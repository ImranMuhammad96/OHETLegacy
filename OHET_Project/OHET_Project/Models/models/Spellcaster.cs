using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Spellcaster
    {

        public Spellcaster()
        {
            this.spells = new HashSet<Spell>();
        }

        [Key]
        [ForeignKey("IDClass")]
        public Class Class { get; set; }
        public int? IDClass { get; set; }

        public virtual ICollection<Spell> spells { get; set; }
    }
}