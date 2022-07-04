using Microsoft.EntityFrameworkCore;
using Tremplin.Data;
using Tremplin.Data.Entity.Consultation;
using Tremplin.IRepositories.IConsultation;

namespace Tremplin.Repositories
{
    public class ConsultationRepository<T> : IConsultationRepository<T> where T : Consultation
    {
        private DataContext DataContext { get; init; }

        public ConsultationRepository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public T GetConsultationById(int id)
        {
            return DataContext.Set<T>().Find(id);
        }

        public IQueryable<T> GetConsultations()
        {
            DbSet<T> consultations = DataContext.Set<T>();

            return consultations;
        }

        public void CreateConsultation(T consultation)
        {
            DataContext.Add(consultation);

            DataContext.SaveChanges();
        }

        public void UpdateConsultation(T consultation)
        {
            DataContext.Update(consultation);

            DataContext.SaveChanges();
        }

        public void DeleteConsultation(T consultation)
        {
            DataContext.Remove(consultation);

            DataContext.SaveChanges();
        }
    }
}