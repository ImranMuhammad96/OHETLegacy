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
        public DbSet<Class> classes { get; set; }
        public DbSet<Content> contents { get; set; }
        public DbSet<Fistcaster> fistcasters { get; set; }
        public DbSet<Hero> heroes { get; set; }
        public DbSet<Monster> monsters { get; set; }
        public DbSet<Rule> rules { get; set; }
        public DbSet<Spell> spells { get; set; }
        public DbSet<Spellcaster> spellcasters { get; set; }

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hero>()
                .HasMany<Class>(s => s.classes)
                .WithMany(c => c.heroes)
                .Map(cs =>
                {
                    cs.MapLeftKey("HeroRefId");
                    cs.MapRightKey("ClassRefId");
                    cs.ToTable("CH");
                });

        }
        
    }
}