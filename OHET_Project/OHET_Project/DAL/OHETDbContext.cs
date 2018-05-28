using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OHET_Project.Models.models;

namespace OHET_Project.DAL
{
    public class OHETDbContext : DbContext
    {

        public OHETDbContext()
            : base("DefaultConnection")
        {
            
        }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Adventure> Adventures { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Attribute> Attributes { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Concept> Concepts { get; set; }


    }
}