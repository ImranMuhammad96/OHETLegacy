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
    public class Class
    {
        public Class()
        {
            this.heroes = new Collection<Hero>();
            this.abilities = new Collection<Ability>();
        }
        
        [Key]
        public int IDClass { get; set; }

        [Required]
        public string name { get; set; }

        public string description { get; set; }

        public bool isSpellcaster { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }

        public virtual ICollection<Hero> heroes { get; set; }
        public virtual ICollection<Ability> abilities { get; set; }
    }
}