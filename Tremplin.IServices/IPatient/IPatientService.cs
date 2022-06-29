using Tremplin.Core.Enums;
using Tremplin.Data;

namespace Tremplin.IServices.IPatient
{
    public interface IPatientService
    {
        #region CRUD Patients

        Patient GetPatientById (int id);

        /// <summary>
        /// Gets list of patients with shared sheet and/or created by logged user
        /// </summary>
        /// <param name="userName">Logged user</param>
        IQueryable<Patient> GetPatients(string userName);

        /// <summary>
        /// Creation of a patient by logged user
        /// </summary>
        /// <param name="userName">Logged user</param>
        void CreatePatient(string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet, string userName);

        void UpdatePatient(Patient patient, string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet);

        void DeletePatient(Patient patient);

        #endregion CRUD Patients
    }
}