using Tremplin.Models;

namespace Tremplin.IServices.IConsultation
{
    public interface IConsultationService
    {
        #region CRUD Consultations

        ConsultationModel GetConsultationById(int id);

        /// <summary>
        /// Gets list of consultations by patient Id
        /// </summary>
        IEnumerable<ConsultationModel> GetConsultations(int idPatient);

        /// <summary>
        /// Creation of a consultation for the selected patient
        /// </summary>
        void CreateConsultation(DateTime date, string shortDescription, string? longDescription, int patientId);

        /// <summary>
        /// Update of a consultation for the selected patient
        /// </summary>
        void UpdateConsultation(ConsultationModel consultationModel, DateTime date, string shortDescription, string? longDescription);

        void DeleteConsultation(ConsultationModel consultationModel);

        #endregion CRUD Consultations
    }
}