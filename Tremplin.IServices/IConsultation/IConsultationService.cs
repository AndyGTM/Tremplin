using Tremplin.Models;

namespace Tremplin.IServices
{
    public interface IConsultationService
    {
        #region CRUD Consultations

        ConsultationModel GetConsultationById(int idConsultation);

        IEnumerable<ConsultationModel> GetConsultations(int idPatient);

        void CreateConsultation(ConsultationModel consultationModel);

        void UpdateConsultation(ConsultationModel consultationModel);

        void DeleteConsultation(ConsultationModel consultationModel);

        #endregion CRUD Consultations
    }
}