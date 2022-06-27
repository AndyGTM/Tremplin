using Tremplin.Data;

namespace Tremplin.IServices.IConsultation
{
    public interface IConsultationService
    {
        /// <summary>
        /// Gets list of consultations by patient Id
        /// </summary>
        IQueryable<Consultation> GetConsultations(int idPatient);
    }
}