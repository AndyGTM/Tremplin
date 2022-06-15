using System.Text.RegularExpressions;

namespace Tremplin.Tests
{
    [TestClass]
    public class PatientTest
    {
        /// <summary>
        /// Test if birth date is not after today's date
        /// </summary>
        [TestMethod]
        public void Compare_BirthDate_NotAfterToday()
        {
            // Arrange
            DateTime birthDate = new DateTime(1995, 04, 30);

            // Act
            int result = DateTime.Compare(birthDate, DateTime.Today);

            // Assert
            Assert.IsTrue(result <= 0, "Birth date is after today's date");
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
    }
}