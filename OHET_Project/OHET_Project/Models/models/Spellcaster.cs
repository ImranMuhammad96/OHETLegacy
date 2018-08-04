using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Spellcaster : Class
    {
        /*
        public Spellcaster()
        {
            this.spells = new HashSet<Spell>();
        }
        */

        public virtual ICollection<Spell> spells { get; set; }
    }
}