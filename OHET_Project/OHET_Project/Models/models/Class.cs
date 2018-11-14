﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    [DebuggerStepThrough]
    public class Class
    {
        public Class()
        {
            this.heroes = new HashSet<Hero>();
        }
        
        [Key]
        public int IDClass { get; set; }

        [Required]
        public string name { get; set; }

        public string description { get; set; }

        [ForeignKey("IDContent")]
        public Content Content { get; set; }
        public int? IDContent { get; set; }

        public virtual ICollection<Hero> heroes { get; set; }
        public virtual ICollection<Ability> abilities { get; set; }
    }
}