using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    [DebuggerStepThrough]
    public class Spellcaster : Class
    {
        public Spellcaster()
        {
            this.spells = new Collection<Spell>();
        }
        public virtual ICollection<Spell> spells { get; set; }
    }
}