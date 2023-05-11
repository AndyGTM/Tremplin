using Moq;
using Tremplin.Core.Enums;
using Tremplin.IRepositories;
using Tremplin.IServices;
using Tremplin.Models;
using Tremplin.Services;

namespace Tremplin.Tests.Services.Patient
{
    [TestClass]
    public class PatientServiceTest
    {
        private readonly Mock<IPatientRepository<Data.Entity.Patient>> _patientRepositoryMock;
        private readonly IPatientService _patientService;

        public PatientServiceTest()
        {
            _patientRepositoryMock = new();
            _patientService = new PatientService(_patientRepositoryMock.Object);
        }

        [TestMethod("Correctly call the service to get a patient by his id")]
        public void Get_PatientById_CallServiceCorrectly()
        {
            Data.Entity.Patient patientMock = new()
            {
                Id = 20,
                SocialSecurityNumber = "175043577788855",
                LastName = "Julio",
                FirstName = "Fabrice",
                BirthDate = new DateTime(1975, 04, 30),
                BloodGroup = BloodGroupNames.APositive,
                Sex = SexTypes.Male,
                SharedSheet = true,
                CreatedBy = "Antoine"
            };

            _patientService.GetPatientById(patientMock.Id);

            _patientRepositoryMock.Verify(p => p.GetPatientById(patientMock.Id), Times.Once());
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

        [TestMethod("Correctly call the service to create a patient")]
        public void Create_Patient_CallServiceCorrectly()
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

            _patientService.CreatePatient(patientModelMock);

            _patientRepositoryMock.Verify(p => p.CreatePatient(It.IsAny<Data.Entity.Patient>()), Times.Once());
        }

        [TestMethod("Correctly call the service to delete a patient")]
        public void Delete_Patient_CallServiceCorrectly()
        {
            PatientModel patientModelMock = new()
            {
                Id = 64,
                SocialSecurityNumber = "183061468623648",
                LastName = "Fitou",
                FirstName = "Marc",
                BirthDate = new DateTime(1983, 06, 14),
                BloodGroup = BloodGroupNames.BPositive,
                Sex = SexTypes.Male,
                SharedSheetWithOthersPractitioners = true,
                CreatedBy = "Antoine"
            };

            _patientService.DeletePatient(patientModelMock);

            _patientRepositoryMock.Verify(p => p.DeletePatient(It.IsAny<Data.Entity.Patient>()), Times.Once());
        }
    }
}