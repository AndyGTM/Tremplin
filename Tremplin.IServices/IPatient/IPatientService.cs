using Tremplin.Core.Enums;
using Tremplin.Data;

namespace Tremplin.IServices.IPatient
{
    public interface IPatientService
    {
        IQueryable<Patient> GetPatients(User user);

        Patient CreatePatient(string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet, string userName);
    }
}