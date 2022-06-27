using Tremplin.Core.Enums;
using Tremplin.Data;

namespace Tremplin.IServices.IPatient
{
    public interface IPatientService
    {
        #region CRUD Patients

        IQueryable<Patient> GetPatients(string userName);

        void CreatePatient(string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet, string userName);

        void UpdatePatient(Patient patient, string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet);

        #endregion CRUD Patients

        #region Format social security number

        string AddBlankSpacesInSocialSecurityNumber(string socialSecurityNumber);
        
        string RemoveBlankSpacesInSocialSecurityNumber(string socialSecurityNumber);

        #endregion Format social security number
    }
}