using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PyramidPlaningSystem.Models
{
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

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
    }
}