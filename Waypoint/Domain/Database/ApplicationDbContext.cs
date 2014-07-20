﻿using System.Data.Entity;

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
        public DbSet<ApiToken> ApiTokens { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public DbSet<UserLocationReason> UserLocationReasons { get; set; }
    }
}
