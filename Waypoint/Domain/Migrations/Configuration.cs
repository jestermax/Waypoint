using System;

using Domain.Configuration;
using Domain.Database;
using Domain.Models;

namespace Domain.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            context.Accounts.AddOrUpdate(
                new Account
                {
                    Id = AppConfiguration.WaypointAccountId.ToString(),
                    Name = "Waypoint",
                    DateCreated = new DateTime(2014, 7, 19, 23, 02, 0).ToUniversalTime(),
                    DateModified = DateTime.UtcNow
                });

            context.Countries.AddOrUpdate(
                new Country
                {
                    Id = KnownId.CountryCanadaId,
                    Name = "Canada"
                },
                new Country
                {
                    Id = KnownId.CountryUnitedStatesId,
                    Name = "United States"
                },
                new Country
                {
                    Id = KnownId.CountryMexicoId,
                    Name = "Mexico"
                });
        }
    }
}
