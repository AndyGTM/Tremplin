using System.Text.RegularExpressions;

namespace Tremplin.Tests
{
    [TestClass]
    public class PatientTest
    {
        #region Tests

        /// <summary>
        /// Comparison test between birth date and today's date
        /// </summary>
        /// /// <param name="comparisonExpected">Return -1 if BirthDate is earlier than Today, 1 if BirthDate is later than Today and 0 if the two dates are equal</param>
        [TestMethod]
        // Arrange
        [DynamicData(nameof(ComparisonDatesData), DynamicDataSourceType.Method)]
        public void Compare_BirthDate_WithToday(DateTime birthDate, int comparisonExpected)
        {
            // Act
            int result = DateTime.Compare(birthDate, DateTime.Today);

            // Assert
            Assert.AreEqual(comparisonExpected, result);
        }

        /// <summary>
        /// Test if blank spaces in social security number are correctly removed
        /// </summary>
        [TestMethod]
        public void Format_SocialSecurityNumber_RemoveBlankSpaces()
        {
            // Arrange
            string socialSecurityNumber = "1 90 26 35 777 888 55";

            // Act
            string result = Regex.Replace(socialSecurityNumber, @"\s", "");

            // Assert
            Assert.AreEqual("190263577788855", result);
        }

        #endregion Tests

        #region Data

        /// <summary>
        /// Collection of objects, with dates and integers used for testing the "DateTime.Compare()" method
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<object[]> ComparisonDatesData()
        {
            // Earlier date than today (integer value is then equal to -1 when using "DateTime.Compare()")
            yield return new object[] { new DateTime(1995, 04, 30), -1};

            // Same date as today (integer value is then equal to 0 when using "DateTime.Compare()")
            yield return new object[] { DateTime.Today, 0 };

            // Later date than today (integer value is then equal to 1 when using "DateTime.Compare()")
            yield return new object[] { new DateTime(3095, 04, 30), 1 };
        }

        #endregion Data
    }
}