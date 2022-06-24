using Tremplin.Data;

namespace Tremplin.IServices.IPatient
{
    public interface IPatientService
    {
        IQueryable<Patient> GetPatients(User user);
    }
}