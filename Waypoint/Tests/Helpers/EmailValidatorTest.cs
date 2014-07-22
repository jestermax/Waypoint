using Microsoft.VisualStudio.TestTools.UnitTesting;

using Domain.Helpers;

namespace Tests.Helpers
{
    [TestClass]
    public class EmailValidatorTest
    {
        [TestMethod]
        public void AttemptEmails()
        {
            const string blankEmail = "";
            const string nullEmail = null;
            const string missingAddress = "@waypoint.io";
            const string missingDomain = "johnsmith@.io";
            const string missingTld = "johnsmith@waypoint";
            const string misingAtSign = "johnsmith.io";
            const string justAddress = "johnsmith";
            const string multipleAtSigns1 = "john@smith@waypoint.io";
            const string multipleAtSigns2 = "johnsmith@@waypoint.io";
            const string invalidStart = ".johnsmith@waypoint.io";
            const string repeatedDomainDots = "johnsmith@waypoint..io";
            const string alexsimmsGmail = "alexsimms@gmail.com";
            const string alexTrackem1 = "alex@trackem.com";
            const string alexTrackem2 = "alex@trackem.ca";
            const string alexSim = "alex@solutionsintomotion.com";

            const string normalEmail = "unittests@waypoint.io";
            const string hasSubdomain = "unittests@unittests.waypoint.io";

            Assert.IsFalse(EmailValidator.IsValid(blankEmail));
            Assert.IsFalse(EmailValidator.IsValid(nullEmail));
            Assert.IsFalse(EmailValidator.IsValid(missingAddress));
            Assert.IsFalse(EmailValidator.IsValid(missingDomain));
            Assert.IsFalse(EmailValidator.IsValid(missingTld));
            Assert.IsFalse(EmailValidator.IsValid(misingAtSign));
            Assert.IsFalse(EmailValidator.IsValid(justAddress));
            Assert.IsFalse(EmailValidator.IsValid(multipleAtSigns1));
            Assert.IsFalse(EmailValidator.IsValid(multipleAtSigns2));
            Assert.IsFalse(EmailValidator.IsValid(invalidStart));
            Assert.IsFalse(EmailValidator.IsValid(repeatedDomainDots));
            Assert.IsFalse(EmailValidator.IsValid(alexsimmsGmail));
            Assert.IsFalse(EmailValidator.IsValid(alexTrackem1));
            Assert.IsFalse(EmailValidator.IsValid(alexTrackem2));
            Assert.IsFalse(EmailValidator.IsValid(alexSim));

            Assert.IsTrue(EmailValidator.IsValid(normalEmail));
            Assert.IsTrue(EmailValidator.IsValid(hasSubdomain));
        }
    }
}
