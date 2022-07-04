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

        public ConsultationModel GetConsultationById(int id)
        {
            Consultation consultation = _consultationRepository.GetConsultationById(id);

            ConsultationModel consultationModel = MapToConsultationModel(consultation);

            return consultationModel;
        }

        /// <summary>
        /// Gets list of consultations by patient Id
        /// </summary>
        public IEnumerable<ConsultationModel> GetConsultations(int idPatient)
        {
            IQueryable<Consultation> consultations = _consultationRepository.GetConsultations().Where(m => m.PatientId == idPatient);

            List<ConsultationModel> consultationsModels = new();

            foreach (Consultation consultation in consultations)
            {
                ConsultationModel consultationModel = MapToConsultationModel(consultation);
                consultationsModels.Add(consultationModel);
            }

            return consultationsModels;
        }

        /// <summary>
        /// Creation of a consultation for the selected patient
        /// </summary>
        public void CreateConsultation(DateTime date, string shortDescription, string? longDescription, int patientId)
        {
            // Consultation creation
            ConsultationModel consultationModel = new()
            {
                Date = date,
                ShortDescription = shortDescription,
                LongDescription = longDescription,
                PatientId = patientId
            };

            Consultation consultation = MapToConsultation(consultationModel);

            _consultationRepository.CreateConsultation(consultation);
        }

        /// <summary>
        /// Update of a consultation for the selected patient
        /// </summary>
        public void UpdateConsultation(ConsultationModel consultationModel, DateTime date, string shortDescription, string? longDescription)
        {
            // Consultation update
            consultationModel.Date = date;
            consultationModel.ShortDescription = shortDescription;
            consultationModel.LongDescription = longDescription;

            Consultation consultation = MapToConsultation(consultationModel);

            _consultationRepository.UpdateConsultation(consultation);
        }

        public void DeleteConsultation(ConsultationModel consultationModel)
        {
            Consultation consultation = _consultationRepository.GetConsultationById(consultationModel.Id);

            _consultationRepository.DeleteConsultation(consultation);
        }

        #endregion CRUD Consultations

        #region Mapping

        /// <summary>
        /// Map consultation model to consultation entity
        /// </summary>
        /// <param name="consultationModel">Consultation model</param>
        /// <returns>Consultation entity</returns>
        public Consultation MapToConsultation(ConsultationModel consultationModel)
        {
            if (consultationModel == null)
                return new Consultation();

            Consultation consultation = _consultationRepository.GetConsultationById(consultationModel.Id);

            if (consultation == null)
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

            consultation.Date = consultationModel.Date;
            consultation.ShortDescription = consultationModel.ShortDescription;
            consultation.LongDescription = consultationModel.LongDescription;
            consultation.PatientId = consultationModel.PatientId;

            return consultation;
        }

        /// <summary>
        /// Map consultation entity to consultation model
        /// </summary>
        /// <param name="consultation">Consultation entity</param>
        /// <returns>Consultation model</returns>
        public static ConsultationModel MapToConsultationModel(Consultation consultation)
        {
            if (consultation == null)
                return new ConsultationModel();

            return new ConsultationModel()
            {
                Id = consultation.Id,
                Date = consultation.Date,
                ShortDescription = consultation.ShortDescription,
                LongDescription = consultation.LongDescription,
                PatientId = consultation.PatientId
            };
        }

        #endregion Mapping
    }
}