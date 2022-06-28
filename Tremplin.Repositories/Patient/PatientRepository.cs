using Microsoft.EntityFrameworkCore;
using Tremplin.Data;
using Tremplin.IRepositories.IPatient;

namespace Tremplin.Repositories
{
    public class PatientRepository<T> : IPatientRepository<T> where T : Patient
    {
        private DataContext DataContext { get; init; }

        public PatientRepository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public IQueryable<T> GetPatients()
        {
            DbSet<T> patients = DataContext.Set<T>();

            return patients;
        }
    }
}