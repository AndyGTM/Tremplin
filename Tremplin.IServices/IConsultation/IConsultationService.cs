using Tremplin.Models;

namespace Tremplin.IServices
{
    public interface IConsultationService
    {
        #region CRUD Consultations

        ConsultationModel GetConsultationById(int consultationId);

        IEnumerable<ConsultationModel> GetConsultations(int patientId);

        void CreateConsultation(ConsultationModel consultationModel);

        void UpdateConsultation(ConsultationModel consultationModel);

        void DeleteConsultation(ConsultationModel consultationModel);

        #endregion CRUD Consultations
    }
}