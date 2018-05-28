using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

    }
}