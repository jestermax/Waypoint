using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain.Models;

namespace Tests.Models
{
    [TestClass]
    public class StaticCreateTest
    {
        [TestMethod]
        public void CreatePlaceTest()
        {
            var id = Guid.NewGuid().ToString();
            const string name = "Create place test";

            var place = Place.Create(id, name, null, null);
        }
    }
}
