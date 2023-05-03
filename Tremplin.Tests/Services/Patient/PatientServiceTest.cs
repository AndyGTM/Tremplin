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
            patientServiceMock.Setup(p => p.GetPatientById(patientModelMock.Id))
                              .Returns(patientModelMock);

            PatientModel patientModelResult = patientServiceMock.Object.GetPatientById(patientModelMock.Id);

            Assert.AreEqual(patientModelMock, patientModelResult);
        }

        [TestMethod("Get patients with shared sheet and/or created by defined user")]
        public void Get_Patients_WithSharedSheetAndOrCreatedByDefinedUser()
        {
            string definedUser = "Antoine";
            List<PatientModel> patientsModelsMock = new(){
                new PatientModel
                {
                    Id = 30,
                    SocialSecurityNumber = "164031685324945",
                    LastName = "Bollou",
                    FirstName = "Michel",
                    BirthDate = new DateTime(1964, 03, 07),
                    BloodGroup = BloodGroupNames.BPositive,
                    Sex = SexTypes.Male,
                    SharedSheetWithOthersPractitioners = true,
                    CreatedBy = definedUser
                },
                new PatientModel
                {
                    Id = 31,
                    SocialSecurityNumber = "259075841653541",
                    LastName = "Fadois",
                    FirstName = "Marthe",
                    BirthDate = new DateTime(1959, 07, 11),
                    BloodGroup = BloodGroupNames.ABPositive,
                    Sex = SexTypes.Female,
                    SharedSheetWithOthersPractitioners = false,
                    CreatedBy = definedUser
                },
                new PatientModel
                {
                    Id = 32,
                    SocialSecurityNumber = "271022365485214",
                    LastName = "Saffa",
                    FirstName = "Denise",
                    BirthDate = new DateTime(1971, 02, 02),
                    BloodGroup = BloodGroupNames.OPositive,
                    Sex = SexTypes.Female,
                    SharedSheetWithOthersPractitioners = true,
                    CreatedBy = "Paul"
                },
                new PatientModel
                {
                    Id = 33,
                    SocialSecurityNumber = "178114368568423",
                    LastName = "Gupain",
                    FirstName = "Fabrice",
                    BirthDate = new DateTime(1978, 11, 25),
                    BloodGroup = BloodGroupNames.ANegative,
                    Sex = SexTypes.Male,
                    SharedSheetWithOthersPractitioners = false,
                    CreatedBy = "Paul"
                }
            };
            Mock<IPatientService> patientServiceMock = new();
            patientServiceMock.Setup(p => p.GetPatients(definedUser))
                              .Returns(patientsModelsMock.Where(m => m.SharedSheetWithOthersPractitioners || m.CreatedBy == definedUser));

            IEnumerable<PatientModel> patientsModelsResult = patientServiceMock.Object.GetPatients(definedUser);

            Assert.AreEqual(3, patientsModelsResult.Count());
        }

        [TestMethod("Create a patient correctly")]
        public void Create_Patient_Correctly()
        {
            PatientModel patientModelMock = new()
            {
                Id = 45,
                SocialSecurityNumber = "264012685413579",
                LastName = "Camblet",
                FirstName = "Gabrielle",
                BirthDate = new DateTime(1964, 01, 24),
                BloodGroup = BloodGroupNames.BNegative,
                Sex = SexTypes.Female,
                SharedSheetWithOthersPractitioners = false,
                CreatedBy = "Antoine"
            };
            Mock<IPatientService> patientServiceMock = new();

            patientServiceMock.Object.CreatePatient(patientModelMock);

            patientServiceMock.Verify(p => p.CreatePatient(It.IsAny<PatientModel>()), Times.Once());
        }
    }
}