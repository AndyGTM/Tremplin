using System.Text.RegularExpressions;

namespace Tremplin.Tests
{
    [TestClass]
    public class PatientTest
    {
        /// <summary>
        /// Test if blank spaces in social security number are correctly removed
        /// </summary>
        [TestMethod]
        public void Format_SocialSecurityNumber_RemoveBlankSpaces()
        {
            // Arrange
            string socialSecurityNumber = "1 90 26 35 777 888 55";

            // Act
            string actual = Regex.Replace(socialSecurityNumber, @"\s", "");

            // Assert
            Assert.AreEqual("190263577788855", actual);
        }
    }
}