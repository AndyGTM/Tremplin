using Tremplin.Core.Helpers;
using Tremplin.Tests.CustomData;

namespace Tremplin.Tests
{
    [TestClass]
    public class PatientTest
    {
        [DataTestMethod]
        [ComparisonDatesDataSource]
        public void Compare_BirthDate_WithToday(string displayTestRowName, DateTime birthDate, int expectedComparison)
        {
            int currentComparison = DateTime.Compare(birthDate, DateTime.Today);

            Assert.AreEqual(expectedComparison, currentComparison);
        }

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