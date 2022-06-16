using System.Text.RegularExpressions;
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
        /// Test if blank spaces in social security number are correctly removed
        /// </summary>
        [TestMethod("Remove blank spaces from social security number")]
        public void Format_SocialSecurityNumber_RemoveBlankSpaces()
        {
            // Arrange
            string socialSecurityNumber = "1 90 26 35 777 888 55";

            // Act
            string result = Regex.Replace(socialSecurityNumber, @"\s", "");

            // Assert
            Assert.AreEqual("190263577788855", result);
        }
    }
}