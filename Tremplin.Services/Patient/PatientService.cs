using Tremplin.Core.Enums;
using Tremplin.Core.Helpers;
using Tremplin.Data;
using Tremplin.IRepositories.IPatient;
using Tremplin.IServices.IPatient;

namespace Tremplin.Services
{
    public class PatientService : IPatientService
    {
        private IPatientRepository<Patient> _patientRepository { get; set; }

        public PatientService(IPatientRepository<Patient> patientRepository)
        {
            _patientRepository = patientRepository;
        }

        #region CRUD Patients

        public Patient GetPatientById(int id)
        {
            Patient patient = _patientRepository.GetPatientById(id);

            return patient;
        }

        /// <summary>
        /// Gets list of patients with shared sheet and/or created by logged user
        /// </summary>
        /// <param name="userName">Logged user</param>
        public IQueryable<Patient> GetPatients(string userName)
        {
            IQueryable<Patient> patients = _patientRepository.GetPatients().Where(m => m.SharedSheet || m.CreatedBy == userName);

            return patients;
        }

        /// <summary>
        /// Creation of a patient by logged user
        /// </summary>
        /// <param name="userName">Logged user</param>
        public void CreatePatient(string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet, string userName)
        {
            Patient patient = new()
            {
                // Removal of any blank spaces for recording the social security number in the database
                SocialSecurityNumber = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(socialSecurityNumber),

                LastName = lastName,
                FirstName = firstName,
                BirthDate = birthDate,
                BloodGroup = bloodGroup,
                Sex = sex,
                SharedSheet = sharedSheet,
                CreatedBy = userName
            };

            _patientRepository.CreatePatient(patient);
        }

        public void UpdatePatient(Patient patient, string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet)
        {
            // Removal of any blank spaces for recording the social security number in the database
            patient.SocialSecurityNumber = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(socialSecurityNumber);

            patient.LastName = lastName;
            patient.FirstName = firstName;
            patient.BirthDate = birthDate;
            patient.BloodGroup = bloodGroup;
            patient.Sex = sex;
            patient.SharedSheet = sharedSheet;

            _patientRepository.UpdatePatient(patient);
        }

        public void DeletePatient(Patient patient)
        {
            _patientRepository.DeletePatient(patient);
        }

        #endregion CRUD Patients
    }
}