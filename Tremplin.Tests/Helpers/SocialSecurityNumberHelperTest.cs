using Tremplin.Core.Helpers;

namespace Tremplin.Tests.Helpers
{
    [TestClass]
    public class SocialSecurityNumberHelperTest
    {
        [TestMethod("Add blank spaces in social security number")]
        public void Format_SocialSecurityNumber_AddBlankSpaces()
        {
            string socialSecurityNumber = "289113356951934";

            string result = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(socialSecurityNumber);

            Assert.AreEqual("2 89 11 33 569 519 34", result);
        }

        [TestMethod("Remove blank spaces in social security number")]
        public void Format_SocialSecurityNumber_RemoveBlankSpaces()
        {
            string socialSecurityNumber = "1 90 05 35 777 888 55";

            string result = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(socialSecurityNumber);

            Assert.AreEqual("190053577788855", result);
        }
    }
}