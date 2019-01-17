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
        public DbSet<Comment> comments { get; set; }
        public DbSet<Content> contents { get; set; }
        public DbSet<FavCon> favcons { get; set; }
        public DbSet<Hero> heroes { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<Monster> monsters { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<Rule> rules { get; set; }
        public DbSet<Spell> spells { get; set; }
        public DbSet<Subpost> subposts { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subpost>()
            .HasRequired(p => p.Post)
            .WithMany(p => p.subposts)
            .WillCascadeOnDelete(true);

            modelBuilder.Entity<Comment>()
            .HasRequired(p => p.Post)
            .WithMany(p => p.comments)
            .WillCascadeOnDelete(true);
        }
    }
}