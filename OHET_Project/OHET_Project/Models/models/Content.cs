using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Content
    {
        [Key]
        public int IDContent { get; set; }

        [ForeignKey("IDUser")]
        public User User { get; set; }
        public int? IDUser { get; set; }

        [Required]
        public bool isOfficial { get; set; }

        [Required]
        public bool isPublic { get; set; }

        public ICollection<Monster> Monsters { get; set; }
        public ICollection<Class> Classes { get; set; }
        public ICollection<Hero> Heros { get; set; }
        public ICollection<Spell> Spells { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<Rule> Rules { get; set; }
        public ICollection<Adventure> Adventures { get; set; }
    }
}