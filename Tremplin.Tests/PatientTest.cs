using Tremplin.Core.Helpers;
using Tremplin.Tests.CustomData;

namespace Tremplin.Tests
{
    [TestClass]
    public class PatientTest
    {
        /// <summary>
        /// Comparison test between birth date and today's date
        /// </summary>
        /// <param name="comparisonExpected">Return -1 if BirthDate is earlier than Today, 1 if BirthDate is later than Today and 0 if the two dates are equal</param>
        [DataTestMethod]
        // Arrange
        [ComparisonDatesDataSource]
        public void Compare_BirthDate_WithToday(DateTime birthDate, int comparisonExpected, string displayName)
        {
            // Act
            int result = DateTime.Compare(birthDate, DateTime.Today);

            // Assert
            Assert.AreEqual(comparisonExpected, result);
        }

        /// <summary>
        /// Test if blank spaces in social security number are correctly added
        /// </summary>
        [TestMethod("Add blank spaces in social security number")]
        public void Format_SocialSecurityNumber_AddBlankSpaces()
        {
            // Arrange
            string socialSecurityNumber = "289113356951934";

            // Act
            string result = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(socialSecurityNumber);

            // Assert
            Assert.AreEqual("2 89 11 33 569 519 34", result);
        }

        /// <summary>
        /// Test if blank spaces in social security number are correctly removed
        /// </summary>
        [TestMethod("Remove blank spaces in social security number")]
        public void Format_SocialSecurityNumber_RemoveBlankSpaces()
        {
            // Arrange
            string socialSecurityNumber = "1 90 05 35 777 888 55";

            // Act
            string result = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(socialSecurityNumber);

            // Assert
            Assert.AreEqual("190053577788855", result);
        }
    }
}