using Moq;
using Tremplin.Core.Enums;
using Tremplin.Core.Helpers;
using Tremplin.IServices;
using Tremplin.Models;
using Tremplin.Tests.CustomData;

namespace Tremplin.Tests
{
    [TestClass]
    public class PatientTest
    {
        [TestMethod("Get a patient by his id")]
        public void Get_Patient_ById()
        {
            PatientModel patientModelMock = new()
            {
                Id = 20,
                SocialSecurityNumber = "175043577788855",
                LastName = "Julio",
                FirstName = "Fabrice",
                BirthDate = new DateTime(1975, 04, 30),
                BloodGroup = BloodGroupNames.APositive,
                Sex = SexTypes.Male,
                SharedSheetWithOthersPractitioners = true,
                CreatedBy = "Antoine"
            };
            Mock<IPatientService> patientServiceMock = new();
            patientServiceMock.Setup(p => p.GetPatientById(patientModelMock.Id)).Returns(patientModelMock);

            PatientModel patientModelResult = patientServiceMock.Object.GetPatientById(patientModelMock.Id);

            Assert.AreEqual(patientModelMock, patientModelResult);
        }

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