using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.KnownId
{
    [TestClass]
    public class KnownIdTest
    {
        [TestMethod]
        public void CheckCountryIdTest()
        {
            Assert.AreEqual(new Guid("f95f1706-f04b-4f45-a8d0-6e73bb418214"), Domain.Models.KnownId.CountryCanadaId);
            Assert.AreEqual(new Guid("9f764cdc-f306-41f6-8f87-5839d9b4365a"), Domain.Models.KnownId.CountryUnitedStates);
        }
    }
}
