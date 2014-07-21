using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

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
            // Optional: Attach debugger
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }

            // Seed accounts table
            context.Accounts.AddOrUpdate(new Account
            {
                Id = AppConfiguration.WaypointAccountId,
                Name = "Waypoint",
                DateCreated = new DateTime(2014, 7, 19, 23, 02, 0).ToUniversalTime(),
                DateModified = DateTime.UtcNow
            });

            // Seed users table
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            const string waypointAdministratorRoleName = "Waypoint Administrator";
            const string accountAdministratorRoleName = "Administrator";

            roleManager.Create(new IdentityRole(waypointAdministratorRoleName));
            roleManager.Create(new IdentityRole(accountAdministratorRoleName));

            var waypointAccount = context.Accounts.Find(AppConfiguration.WaypointAccountId);

            AddOrUpdateApplicationUser(context, userManager, new ApplicationUser
            {
                UserName = "adamstirtan@gmail.com",
                Email = "adamstirtan@gmail.com",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                Id = AppConfiguration.AdamUserId,
                LockoutEnabled = false,
                Account = waypointAccount
            }, "Super3vilGenius");

            AddOrUpdateApplicationUser(context, userManager, new ApplicationUser
            {
                UserName = "lesholmes1@sympatico.ca",
                Email = "lesholmes1@sympatico.ca",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                Id = AppConfiguration.LesUserId,
                LockoutEnabled = false,
                Account = waypointAccount
            }, "snowbird", new List<string>
            {
                "accountAdministratorRoleName"
            });

            AddOrUpdateApplicationUser(context, userManager, new ApplicationUser
            {
                UserName = "carlo.giannoccaro@gmail.com",
                Email = "carlo.giannoccaro@gmail.com",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                Id = AppConfiguration.CarloUserId,
                LockoutEnabled = false,
                Account = waypointAccount
            }, "kathryn", new List<String>
            {
                "accountAdministratorRoleName"
            });

            // Seed countries table
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
                }
            );

            base.Seed(context);
        }

        private static void AddOrUpdateApplicationUser(IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> context, UserManager<ApplicationUser, string> userManager, ApplicationUser applicationUser, string password, List<string> roles = null)
        {
            var userExists = context.Users.Any(u => u.Email.Equals(applicationUser.Email));

            if (userExists)
            {
                // TODO: implement update
            }
            else
            {
                var identityResult = userManager.Create(applicationUser, password);

                if (!identityResult.Succeeded)
                {
                    throw new Exception(String.Format("Could not create user: {0}: ", applicationUser.Email));
                }

                if (roles == null)
                {
                    return;
                }

                foreach (var role in roles)
                {
                    userManager.AddToRole(applicationUser.Id, role);
                }
            }
        }
    }
}
