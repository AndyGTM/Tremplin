using Tremplin.Models;

namespace Tremplin.IServices
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
        void CreateConsultation(ConsultationModel consultationModel);

        /// <summary>
        /// Update of a consultation for the selected patient
        /// </summary>
        void UpdateConsultation(ConsultationModel consultationModel);

        void DeleteConsultation(ConsultationModel consultationModel);

        #endregion CRUD Consultations
    }
}