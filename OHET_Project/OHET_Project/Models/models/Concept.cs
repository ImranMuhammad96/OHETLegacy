using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public class Concept
    {
        [Key]
        public int IDConcept { get; set; }

        [Required]
        public int level { get; set; }

        public string description { get; set; }

        public ICollection<Hero> Hero { get; set; }
        public ICollection<Monster> Monster { get; set; }
        public ICollection<Spell> Spell { get; set; }
    }
}