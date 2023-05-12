using Moq;
using Tremplin.Core.Enums;
using Tremplin.IRepositories;
using Tremplin.IServices;
using Tremplin.Services;

namespace Tremplin.Tests.Services.Consultation
{
    [TestClass]
    public class ConsultationServiceTest
    {
        private readonly Mock<IConsultationRepository<Data.Entity.Consultation>> _consultationRepositoryMock;
        private readonly IConsultationService _consultationService;

        public ConsultationServiceTest()
        {
            _consultationRepositoryMock = new();
            _consultationService = new ConsultationService(_consultationRepositoryMock.Object);
        }

        #region CRUD Consultations

        [TestMethod("Correctly call the service to get a consultation by its id")]
        public void Get_ConsultationById_CallServiceCorrectly()
        {
            Data.Entity.Consultation consultationMock = new()
            {
                Id = 20,
                Date = new DateTime(2023, 01, 17),
                ShortDescription = "Migraine",
                LongDescription = null,
                PatientId = 57
            };

            _consultationService.GetConsultationById(consultationMock.Id);

            _consultationRepositoryMock.Verify(p => p.GetConsultationById(consultationMock.Id), Times.Once());
        }

        [TestMethod("Correctly call the service to get consultations from a patient id")]
        public void Get_ConsultationsFromPatientId_CallServiceCorrectly()
        {
            Data.Entity.Patient patientMock = new()
            {
                Id = 65,
                SocialSecurityNumber = "151111245685268",
                LastName = "Korla",
                FirstName = "Didier",
                BirthDate = new DateTime(1951, 11, 21),
                BloodGroup = BloodGroupNames.OPositive,
                Sex = SexTypes.Male,
                SharedSheet = false,
                CreatedBy = "Antoine"
            };

            _consultationService.GetConsultations(patientMock.Id);

            _consultationRepositoryMock.Verify(p => p.GetConsultations(), Times.Once());
        }

        #endregion CRUD Consultations
    }
}