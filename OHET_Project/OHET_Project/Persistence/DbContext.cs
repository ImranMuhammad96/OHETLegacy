using Microsoft.AspNet.Identity.EntityFramework;
using OHET_Project.Models;
using OHET_Project.Models.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OHET_Project.Persistence
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static IdentityDbContext Create()
        {
            return new IdentityDbContext();
        }

        public DbSet<Ability> abilities { get; set; }
        public DbSet<Adventure> adventures { get; set; }
        public DbSet<Armor> armors { get; set; }
        public DbSet<Models.models.Attribute> attributes { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<Concept> concepts { get; set; }
        public DbSet<Content> contents { get; set; }
        public DbSet<Equipment> equipments { get; set; }
        public DbSet<Fistcaster> fistcasters { get; set; }
        public DbSet<Hero> heroes { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Label> labels { get; set; }
        public DbSet<Monster> monsters { get; set; }
        public DbSet<Other> others { get; set; }
        public DbSet<Rule> rules { get; set; }
        public DbSet<Spell> spells { get; set; }
        public DbSet<Spellcaster> spellcasters { get; set; }
        public DbSet<Weapon> weapons { get; set; }

        /*
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Formularz>()
            .HasRequired<Rekrut>(s => s.rekrut)
            .WithMany(g => g.formularze)
            .HasForeignKey<int?>(s => s.rekrutID);

            modelBuilder.Entity<Osoba>().ToTable("Osobas");
            modelBuilder.Entity<Budowniczy>().ToTable("Budowniczies");
            modelBuilder.Entity<CNS>().ToTable("CNSes");
            modelBuilder.Entity<LD>().ToTable("LDs");
            modelBuilder.Entity<Rekrut>().ToTable("Rekruts");
            modelBuilder.Entity<Zarzadca>().ToTable("Zarzadcas");
            modelBuilder.Entity<Zwiadowca>().ToTable("Zwiadowcas");
        }
        */
    }
}