using Tremplin.Data.Entity;
using Tremplin.IRepositories;
using Tremplin.IServices;
using Tremplin.Models;

namespace Tremplin.Services
{
    public class ConsultationService : IConsultationService
    {
        private IConsultationRepository<Consultation> _consultationRepository { get; set; }

        public ConsultationService(IConsultationRepository<Consultation> consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        #region CRUD Consultations

        public ConsultationModel GetConsultationById(int idConsultation)
        {
            Consultation consultationEntity = _consultationRepository.GetConsultationById(idConsultation);

            ConsultationModel consultationModel = MapConsultationEntityToConsultationModel(consultationEntity);

            return consultationModel;
        }

        public IEnumerable<ConsultationModel> GetConsultations(int idPatient)
        {
            IQueryable<Consultation> consultations = _consultationRepository.GetConsultations().Where(m => m.PatientId == idPatient);

            List<ConsultationModel> consultationsModels = new();

            foreach (Consultation consultationEntity in consultations)
            {
                ConsultationModel consultationModel = MapConsultationEntityToConsultationModel(consultationEntity);
                consultationsModels.Add(consultationModel);
            }

            return consultationsModels;
        }

        public void CreateConsultation(ConsultationModel consultationModel)
        {
            Consultation consultationEntity = MapConsultationModelToConsultationEntity(consultationModel);

            _consultationRepository.CreateConsultation(consultationEntity);
        }

        public void UpdateConsultation(ConsultationModel consultationModel)
        {
            Consultation consultationEntity = MapConsultationModelToConsultationEntity(consultationModel);

            _consultationRepository.UpdateConsultation(consultationEntity);
        }

        public void DeleteConsultation(ConsultationModel consultationModel)
        {
            Consultation consultationEntity = _consultationRepository.GetConsultationById(consultationModel.Id);

            _consultationRepository.DeleteConsultation(consultationEntity);
        }

        #endregion CRUD Consultations

        #region Mapping

        public Consultation MapConsultationModelToConsultationEntity(ConsultationModel consultationModel)
        {
            if (consultationModel == null)
                return new Consultation();

            Consultation consultationEntity = _consultationRepository.GetConsultationById(consultationModel.Id);

            if (consultationEntity == null)
            {
                Consultation newConsultation = new()
                {
                    Date = consultationModel.Date,
                    ShortDescription = consultationModel.ShortDescription,
                    LongDescription = consultationModel.LongDescription,
                    PatientId = consultationModel.PatientId
                };

                return newConsultation;
            }

            consultationEntity.Date = consultationModel.Date;
            consultationEntity.ShortDescription = consultationModel.ShortDescription;
            consultationEntity.LongDescription = consultationModel.LongDescription;
            consultationEntity.PatientId = consultationModel.PatientId;

            return consultationEntity;
        }

        public static ConsultationModel MapConsultationEntityToConsultationModel(Consultation consultationEntity)
        {
            if (consultationEntity == null)
                return new ConsultationModel();

            return new ConsultationModel()
            {
                Id = consultationEntity.Id,
                Date = consultationEntity.Date,
                ShortDescription = consultationEntity.ShortDescription,
                LongDescription = consultationEntity.LongDescription,
                PatientId = consultationEntity.PatientId
            };
        }

        #endregion Mapping
    }
}