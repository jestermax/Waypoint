using System;
using System.Data.Entity.Spatial;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain.Configuration;
using Domain.Helpers;
using Domain.Models;
using Domain.Repositories;

namespace Tests.Repositories
{
    [TestClass]
    public class UserLocationRepositoryTest
    {
        [TestMethod]
        public async Task UserLocationRepositoryAddAndRemoveMethod()
        {
            var context = ApplicationDbContext.Create();

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var userLocationRepository = new UserLocationRepository(context);

            const double latitude = 43.43229;
            const double longitude = -79.083293;
            var address = RandomStringGenerator.Create(32);
            var utcNow = DateTime.UtcNow;

            var applicationUser = await userManager.FindAsync(AppConfiguration.UnitTestsEmail, AppConfiguration.UnitTestsPassword);

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual(AppConfiguration.UnitTestsEmail, applicationUser.Email);
            Assert.AreEqual(AppConfiguration.UnitTestsUserId, applicationUser.Id);

            var userLocation = await userLocationRepository.Add(new UserLocation
            {
                Id = Guid.NewGuid().ToString(),
                User = applicationUser,
                Location = DbGeography.PointFromText(String.Format("POINT({1} {0})", latitude, longitude), AppConfiguration.CoordinateSystemId),
                Address = address,
                Accuracy = 10,
                Speed = 10.1,
                DateSent = utcNow,
                DateReceived = utcNow.AddSeconds(1)
            });

            Assert.IsNotNull(userLocation);
            Assert.IsNotNull(userLocation.Id);
            Assert.AreEqual(AppConfiguration.UnitTestsUserId, userLocation.User.Id);
            Assert.AreEqual(latitude, userLocation.Location.Latitude);
            Assert.AreEqual(longitude, userLocation.Location.Longitude);
            Assert.AreEqual(10, userLocation.Accuracy);
            Assert.AreEqual(10.1, userLocation.Speed);
            Assert.AreEqual(utcNow, userLocation.DateSent);
            Assert.AreEqual(utcNow.AddSeconds(1), userLocation.DateReceived);

            var result = await userLocationRepository.Remove(userLocation.Id);

            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public async void UserLocationRepositoryGetyIdMethod()
        //{
        //    var userLocationRepository = new UserLocationRepository();

        //    const double latitude = 43.43229;
        //    const double longitude = -79.083293;
        //    var address = RandomStringGenerator.Create(32);
        //    var utcNow = DateTime.UtcNow;

        //    var userLocation = await userLocationRepository.Add(new UserLocation
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserId = AppConfiguration.UnitTestsUserId,
        //        Location = DbGeography.PointFromText(String.Format("POINT({1} {0})", latitude, longitude), AppConfiguration.CoordinateSystemId),
        //        Address = address,
        //        Accuracy = 50,
        //        Speed = 10.1,
        //        DateSent = utcNow,
        //        DateReceived = utcNow.AddSeconds(1)
        //    });

        //    Assert.IsNotNull(userLocation);
        //    Assert.IsNotNull(userLocation.Id);
        //    Assert.AreEqual(AppConfiguration.UnitTestsUserId, userLocation.UserId);

        //    var retrievedUserLocation = await userLocationRepository.Get(userLocation.Id);

        //    Assert.IsNotNull(retrievedUserLocation);
        //    Assert.IsNotNull(retrievedUserLocation.Id);
        //    Assert.AreEqual(AppConfiguration.UnitTestsUserId, retrievedUserLocation.UserId);
        //    Assert.AreEqual(latitude, retrievedUserLocation.Location.Latitude);
        //    Assert.AreEqual(longitude, retrievedUserLocation.Location.Longitude);
        //    Assert.AreEqual(50, retrievedUserLocation.Accuracy);
        //    Assert.AreEqual(10.1, userLocation.Speed);
        //    Assert.AreEqual(utcNow, retrievedUserLocation.DateSent);
        //    Assert.AreEqual(utcNow.AddSeconds(1), userLocation.DateReceived);

        //    var result = await userLocationRepository.Remove(userLocation.Id);

        //    Assert.IsTrue(result);
        //}

        //[TestMethod]
        //public async void UserLocationRepositoryWhereMethod()
        //{
        //    var userLocationRepository = new UserLocationRepository();

        //    const int count = 5;
        //    const double latitude = 43.45229;
        //    const double longitude = -79.073293;
        //    var address = RandomStringGenerator.Create(32);

        //    for (var i = 0; i < count; i++)
        //    {
        //        var userLocation = await userLocationRepository.Add(new UserLocation
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            UserId = AppConfiguration.UnitTestsUserId,
        //            Location = DbGeography.PointFromText(String.Format("POINT({1} {0})", latitude, longitude), AppConfiguration.CoordinateSystemId),
        //            Address = address,
        //            Accuracy = 12,
        //            Speed = 10,
        //            DateSent = DateTime.UtcNow,
        //            DateReceived = DateTime.UtcNow
        //        });

        //        Assert.IsNotNull(userLocation);
        //    }

        //    var userLocations = await userLocationRepository.Where(l => l.UserId.Equals(AppConfiguration.UnitTestsUserId));

        //    Assert.AreEqual(count, userLocations.Length);
        //    Assert.AreEqual(address, userLocations[0].Address);
        //    Assert.AreEqual(address, userLocations[1].Address);
        //    Assert.AreEqual(address, userLocations[2].Address);
        //    Assert.AreEqual(address, userLocations[3].Address);
        //    Assert.AreEqual(address, userLocations[4].Address);

        //    foreach (var result in userLocations.Select(userLocation => userLocationRepository.Remove(userLocation.Id)))
        //    {
        //        Assert.IsTrue(await result);
        //    }
        //}
    }
}
