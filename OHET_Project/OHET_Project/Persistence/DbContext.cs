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

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Armor>()
                .HasMany<Class>(s => s.classes)
                .WithMany(c => c.armors)
                .Map(cs =>
                {
                    cs.MapLeftKey("ArmorRefId");
                    cs.MapRightKey("ClassRefId");
                    cs.ToTable("CA");
                });
            modelBuilder.Entity<Models.models.Attribute>()
                .HasMany<Hero>(s => s.heroes)
                .WithMany(c => c.attributes)
                .Map(cs =>
                {
                    cs.MapLeftKey("AttributeRefId");
                    cs.MapRightKey("HeroRefId");
                    cs.ToTable("HA");
                });
            modelBuilder.Entity<Hero>()
                .HasMany<Class>(s => s.classes)
                .WithMany(c => c.heroes)
                .Map(cs =>
                {
                    cs.MapLeftKey("HeroRefId");
                    cs.MapRightKey("ClassRefId");
                    cs.ToTable("CH");
                });
            modelBuilder.Entity<Item>()
                .HasMany<Equipment>(s => s.equipments)
                .WithMany(c => c.items)
                .Map(cs =>
                {
                    cs.MapLeftKey("ItemRefId");
                    cs.MapRightKey("EquipmentRefId");
                    cs.ToTable("EI");
                });
            modelBuilder.Entity<Weapon>()
                .HasMany<Class>(s => s.classes)
                .WithMany(c => c.weapons)
                .Map(cs =>
                {
                    cs.MapLeftKey("WeaponRefId");
                    cs.MapRightKey("ClassRefId");
                    cs.ToTable("CW");
                });
            modelBuilder.Entity<Label>()
                .HasMany<Hero>(s => s.heroes)
                .WithMany(c => c.labels)
                .Map(cs =>
                {
                    cs.MapLeftKey("LabelRefId");
                    cs.MapRightKey("HeroRefId");
                    cs.ToTable("HL");
                });
            
            //modelBuilder.Entity<Card>().HasRequired(c => c.Stage).WithMany().WillCascadeOnDelete(false);

            //modelBuilder.Entity<Side>().HasRequired(s => s.Stage).WithMany().WillCascadeOnDelete(false);
            
            /*
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
            */
        }
        
    }
}