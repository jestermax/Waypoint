using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using Domain.Configuration;
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

            context.SaveChanges();

            // Seed timezones table
            context.TimeZones.AddOrUpdate(
                new Models.TimeZone
                {
                    Id = KnownId.TimeZoneHawaiiId,
                    Name = "(GMT-10:00) Hawaii",
                    Offset = -36000,
                    SortOrder = 0
                },
                new Models.TimeZone
                {
                    Id = KnownId.TimeZoneAlaskaId,
                    Name = "(GMT-09:00) Alaska",
                    Offset = -32400,
                    SortOrder = 0
                },
                new Models.TimeZone
                {
                    Id = KnownId.TimeZonePacificId,
                    Name = "(GMT-08:00) Pacific Time (US & Canada)",
                    Offset = -28800,
                    SortOrder = 0
                },
                new Models.TimeZone
                {
                    Id = KnownId.TimeZoneArizonaId,
                    Name = "(GMT-07:00) Arizona",
                    Offset = -25200,
                    SortOrder = 0
                },
                new Models.TimeZone
                {
                    Id = KnownId.TimeZoneMountainId,
                    Name = "(GMT-07:00) Mountain Time (US & Canada)",
                    Offset = -25200,
                    SortOrder = 0
                },
                new Models.TimeZone
                {
                    Id = KnownId.TimeZoneCentralId,
                    Name = "(GMT-06:00) Central Time (US & Canada)",
                    Offset = -21600,
                    SortOrder = 0
                },
                new Models.TimeZone
                {
                    Id = KnownId.TimeZoneEasternId,
                    Name = "(GMT-05:00) Eastern Time (US & Canada)",
                    Offset = -18000,
                    SortOrder = 0
                },
                new Models.TimeZone
                {
                    Id = KnownId.TimeZoneIndianaEastId,
                    Name = "(GMT-05:00) Indiana (East)",
                    Offset = -18000,
                    SortOrder = 0
                }
                );

            context.SaveChanges();

            // Seed accounts table
            context.Accounts.AddOrUpdate(new Account
            {
                Id = AppConfiguration.WaypointAccountId,
                Name = AppConfiguration.WaypointAccountName,
                DateCreated = new DateTime(2014, 7, 19, 23, 02, 0).ToUniversalTime(),
                DateModified = DateTime.UtcNow,
            });

            context.SaveChanges();

            context.Accounts.AddOrUpdate(new Account
            {
                Id = AppConfiguration.UnitTestsAccountId,
                Name = AppConfiguration.UnitTestsAccountName,
                DateCreated = new DateTime(2014, 7, 22, 16, 30, 0).ToUniversalTime(),
                DateModified = DateTime.UtcNow
            });

            context.SaveChanges();

            // Seed users table
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole(AppConfiguration.WaypointAdministratorRoleName));
            roleManager.Create(new IdentityRole(AppConfiguration.AdministratorRoleName));

            var waypointAccount = context.Accounts.Find(AppConfiguration.WaypointAccountId);
            var unitTestsAccount = context.Accounts.Find(AppConfiguration.UnitTestsAccountId);

            var easternTimeZone = context.TimeZones.Find(KnownId.TimeZoneEasternId);

            AddOrUpdateApplicationUser(context, userManager, new ApplicationUser
            {
                UserName = "adamstirtan@gmail.com",
                Email = "adamstirtan@gmail.com",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                Id = AppConfiguration.AdamUserId,
                LockoutEnabled = false,
                Account = waypointAccount,
                TimeZone = easternTimeZone
            }, "Super3vilGenius", new List<string>
            {
                AppConfiguration.WaypointAdministratorRoleName,
                AppConfiguration.AdministratorRoleName
            });

            context.SaveChanges();

            AddOrUpdateApplicationUser(context, userManager, new ApplicationUser
            {
                UserName = "lesholmes1@sympatico.ca",
                Email = "lesholmes1@sympatico.ca",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                Id = AppConfiguration.LesUserId,
                LockoutEnabled = false,
                Account = waypointAccount,
                TimeZone = easternTimeZone
            }, "snowbird", new List<string>
            {
                AppConfiguration.AdministratorRoleName
            });

            context.SaveChanges();

            AddOrUpdateApplicationUser(context, userManager, new ApplicationUser
            {
                UserName = "carlo.giannoccaro@gmail.com",
                Email = "carlo.giannoccaro@gmail.com",
                AccessFailedCount = 0,
                EmailConfirmed = true,
                Id = AppConfiguration.CarloUserId,
                LockoutEnabled = false,
                Account = waypointAccount,
                TimeZone = easternTimeZone
            }, "kathryn", new List<String>
            {
                AppConfiguration.AdministratorRoleName
            });

            context.SaveChanges();

            AddOrUpdateApplicationUser(context, userManager, new ApplicationUser
            {
                UserName = AppConfiguration.UnitTestsEmail,
                Email = AppConfiguration.UnitTestsEmail,
                AccessFailedCount = 0,
                EmailConfirmed = true,
                Id = AppConfiguration.UnitTestsUserId,
                LockoutEnabled = false,
                Account = unitTestsAccount,
                TimeZone = easternTimeZone
            }, AppConfiguration.UnitTestsPassword, new List<string>
            {
                AppConfiguration.AdministratorRoleName
            });

            context.SaveChanges();

            base.Seed(context);
        }

        private static void AddOrUpdateApplicationUser(IdentityDbContext<ApplicationUser, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim> context, UserManager<ApplicationUser, string> userManager, ApplicationUser applicationUser, string password, IEnumerable<string> roles = null)
        {
            var userExists = context.Users.Any(u => u.Email.Equals(applicationUser.Email));

            if (userExists)
            {
                var existing = context.Users.Find(applicationUser.Id);

                if (existing == null)
                {
                    throw new Exception(String.Format("Could not update user: {0}", applicationUser.Email));
                }

                existing.UserName = applicationUser.UserName;
                existing.Email = applicationUser.Email;
                existing.AccessFailedCount = applicationUser.AccessFailedCount;
                existing.EmailConfirmed = applicationUser.EmailConfirmed;
                existing.Id = applicationUser.Id;
                existing.LockoutEnabled = applicationUser.LockoutEnabled;
                existing.Account = applicationUser.Account;
                existing.TimeZone = applicationUser.TimeZone;

                if (roles != null)
                {
                    var existingRoles = userManager.GetRoles(applicationUser.Id);

                    foreach (var existingRole in existingRoles)
                    {
                        userManager.RemoveFromRole(applicationUser.Id, existingRole);
                    }

                    foreach (var role in roles.Where(role => !userManager.IsInRole(applicationUser.Id, role)))
                    {
                        userManager.AddToRole(applicationUser.Id, role);
                    }
                }

                context.Entry(existing).State = EntityState.Modified;
                context.SaveChanges();
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
