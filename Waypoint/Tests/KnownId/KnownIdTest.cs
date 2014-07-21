using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.KnownId
{
    [TestClass]
    public class KnownIdTest
    {
        [TestMethod]
        public void CheckCountryIdTest()
        {
            Assert.AreEqual("f95f1706-f04b-4f45-a8d0-6e73bb418214", Domain.Configuration.KnownId.CountryCanadaId);
            Assert.AreEqual("9f764cdc-f306-41f6-8f87-5839d9b4365a", Domain.Configuration.KnownId.CountryUnitedStatesId);
            Assert.AreEqual("f122e8f1-7b83-4bb3-98e4-d1cab856a605", Domain.Configuration.KnownId.CountryMexicoId);
        }

        [TestMethod]
        public void CheckUserLocationReasonIdTest()
        {
            Assert.AreEqual("7ab098a4-eafc-423b-b77d-487f52504d09", Domain.Configuration.KnownId.UserLocationReasonIntervalId);
        }
    }
}
