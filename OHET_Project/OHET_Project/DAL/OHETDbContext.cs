using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OHET_Project.Models.models;
using System.Data.Entity;

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
        public DbSet<Models.models.Attribute> Attributes { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Concept> Concepts { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<Fistcaster> Fistcasters { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Monster> Monsters { get; set; }
        public DbSet<Other> Others { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Rule> Rules { get; set; }
        public DbSet<Spell> Spells { get; set; }
        public DbSet<Spellcaster> Spellcasters { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
    }
}