using Tremplin.Data.Entity.Patient;

namespace Tremplin.IRepositories.IPatient
{
    public interface IPatientRepository<T> where T : Patient
    {
        T GetPatientById (int id);

        IQueryable<T> GetPatients();

        void CreatePatient(T patient);

        void UpdatePatient(T patient);

        void DeletePatient(T patient);
    }
}