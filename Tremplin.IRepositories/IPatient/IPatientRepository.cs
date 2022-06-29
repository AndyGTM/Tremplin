using Tremplin.Data;

namespace Tremplin.IRepositories.IPatient
{
    public interface IPatientRepository<T> where T : Patient
    {
        IQueryable<T> GetPatients();

        void CreatePatient(T patient);

        void UpdatePatient(T patient);
    }
}