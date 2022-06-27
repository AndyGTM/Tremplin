using Tremplin.Data;
using Tremplin.IServices.IConsultation;

namespace Tremplin.Services
{
    public class ConsultationService : IConsultationService
    {
        private DataContext DataContext { get; init; }

        public ConsultationService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        #region CRUD Consultations

        /// <summary>
        /// Gets list of consultations by patient Id
        /// </summary>
        public IQueryable<Consultation> GetConsultations(int idPatient)
        {
            IQueryable<Consultation> consultations = from m in DataContext.Consultations
                                                     where m.PatientId == idPatient
                                                     select m;
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

            // Adding the consultation to the data context
            DataContext.Add(consultation);
        }

        #endregion CRUD Consultations
    }
}