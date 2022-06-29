using Tremplin.Data;
using Tremplin.IRepositories.IConsultation;
using Tremplin.IServices.IConsultation;

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

        public Consultation GetConsultationById(int id)
        {
            Consultation consultation = _consultationRepository.GetConsultationById(id);

            return consultation;
        }

        /// <summary>
        /// Gets list of consultations by patient Id
        /// </summary>
        public IQueryable<Consultation> GetConsultations(int idPatient)
        {
            IQueryable<Consultation> consultations = _consultationRepository.GetConsultations().Where(m => m.PatientId == idPatient);

            return consultations;
        }

        /// <summary>
        /// Creation of a consultation for the selected patient
        /// </summary>
        public void CreateConsultation(DateTime date, string shortDescription, string? longDescription, int patientId)
        {
            // Consultation creation
            Consultation consultation = new()
            {
                Date = date,
                ShortDescription = shortDescription,
                LongDescription = longDescription,
                PatientId = patientId
            };

            _consultationRepository.CreateConsultation(consultation);
        }

        /// <summary>
        /// Update of a consultation for the selected patient
        /// </summary>
        public void UpdateConsultation(Consultation consultation, DateTime date, string shortDescription, string? longDescription)
        {
            // Consultation update
            consultation.Date = date;
            consultation.ShortDescription = shortDescription;
            consultation.LongDescription = longDescription;

            _consultationRepository.UpdateConsultation(consultation);
        }

        public void DeleteConsultation(Consultation consultation)
        {
            _consultationRepository.DeleteConsultation(consultation);
        }

        #endregion CRUD Consultations
    }
}