using System.Data.Entity;

using Microsoft.AspNet.Identity.EntityFramework;

using Domain.Models;

namespace Domain.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", false)
        { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<UserLocationReason> UserLocationReasons { get; set; }
    }
}
