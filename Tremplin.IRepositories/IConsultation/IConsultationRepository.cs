using Tremplin.Data.Entity;

namespace Tremplin.IRepositories
{
    public interface IConsultationRepository<T> where T : Consultation
    {
        T GetConsultationById(int id);
        
        IQueryable<T> GetConsultations();

        void CreateConsultation(T consultation);

        void UpdateConsultation(T consultation);

        void DeleteConsultation(T consultation);
    }
}