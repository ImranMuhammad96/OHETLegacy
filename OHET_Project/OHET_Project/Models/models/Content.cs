using System;
using System.Collections.Generic;
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
        [Key]
        public int IDContent { get; set; }

        [Required]
        public bool isOfficial { get; set; }

        [Required]
        public bool isPublic { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public string ApplicationUserId { get; set; }

        public ICollection<Monster> monsters { get; set; }
        public ICollection<Class> classes { get; set; }
        public ICollection<Hero> heroes { get; set; }
        public ICollection<Spell> spells { get; set; }
        public ICollection<Rule> rules { get; set; }
        public ICollection<Adventure> adventures { get; set; }
        //public ICollection<ApplicationUser> applicationUsers { get; set; }
    }
}