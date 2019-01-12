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
    public class Content
    {
        public Content()
        {
            this.monsters = new Collection<Monster>();
            this.classes = new Collection<Class>();
            this.heroes = new Collection<Hero>();
            this.spells = new Collection<Spell>();
            this.rules = new Collection<Rule>();
            this.adventures = new Collection<Adventure>();
            this.items = new Collection<Item>();
            this.favcons = new Collection<FavCon>();
        }

        [Key]
        public int IDContent { get; set; }

        [Required]
        public bool isOfficial { get; set; }

        [Required]
        public bool isPublic { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual string ApplicationUserId { get; set; }

        public ICollection<Monster> monsters { get; set; }
        public ICollection<Class> classes { get; set; }
        public ICollection<Hero> heroes { get; set; }
        public ICollection<Spell> spells { get; set; }
        public ICollection<Rule> rules { get; set; }
        public ICollection<Adventure> adventures { get; set; }
        public ICollection<Item> items { get; set; }
        public ICollection<FavCon> favcons { get; set; }
        //public ICollection<ApplicationUser> applicationUsers { get; set; }
    }
}