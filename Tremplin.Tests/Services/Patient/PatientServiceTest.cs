using Moq;
using Tremplin.Core.Enums;
using Tremplin.IServices;
using Tremplin.Models;

namespace Tremplin.Tests.Services.Patient
{
    [TestClass]
    public class PatientServiceTest
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
    }
}