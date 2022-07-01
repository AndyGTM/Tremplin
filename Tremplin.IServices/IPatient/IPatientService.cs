using Tremplin.Core.Enums;
using Tremplin.Models.Patient;

namespace Tremplin.IServices.IPatient
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
        /// <param name="userName">Logged user</param>
        void CreatePatient(string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet, string userName);

        void UpdatePatient(PatientModel patientModel, string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet);

        void DeletePatient(PatientModel patientModel);

        #endregion CRUD Patients
    }
}