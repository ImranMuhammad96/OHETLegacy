﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHET_Project.Models.models
{
    public partial class Class
    {

        public Class()
        {
            this.armors = new HashSet<Armor>();
            this.weapons = new HashSet<Weapon>();
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

        public ICollection<Armor> Armor { get; set; }
        public ICollection<Ability> Ability { get; set; }
        public virtual ICollection<Hero> heroes { get; set; }
        public virtual ICollection<Armor> armors { get; set; }
        public virtual ICollection<Weapon> weapons { get; set; }
    }
}