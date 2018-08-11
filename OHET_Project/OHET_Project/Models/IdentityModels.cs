using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OHET_Project.Models.models;

namespace OHET_Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
    /*
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
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
        public DbSet<User> UsersOHET { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
    }
    */
}