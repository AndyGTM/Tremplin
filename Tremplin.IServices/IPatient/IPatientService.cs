using Tremplin.Models;

namespace Tremplin.IServices
{
    public interface IPatientService
    {
        #region CRUD Patients

        PatientModel GetPatientById (int id);

        /// <summary>
        /// Gets list of patients with shared sheet and/or created by logged user
        /// </summary>
        /// <param name="userName">Logged user</param>
        IEnumerable<PatientModel> GetPatients(string userName);

        /// <summary>
        /// Creation of a patient by logged user
        /// </summary>
        void CreatePatient(PatientModel patientModel);

        void UpdatePatient(PatientModel patientModel);

        void DeletePatient(PatientModel patientModel);

        #endregion CRUD Patients
    }
}