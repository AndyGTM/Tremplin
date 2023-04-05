using Tremplin.Models;

namespace Tremplin.IServices
{
    public interface IPatientService
    {
        #region CRUD Patients

        PatientModel GetPatientById (int patientId);

        IEnumerable<PatientModel> GetPatients(string userName);

        void CreatePatient(PatientModel patientModel);

        void UpdatePatient(PatientModel patientModel);

        void DeletePatient(PatientModel patientModel);

        #endregion CRUD Patients
    }
}