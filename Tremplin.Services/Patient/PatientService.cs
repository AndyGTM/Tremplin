using System.Text.RegularExpressions;
using Tremplin.Core.Enums;
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
                SocialSecurityNumber = RemoveBlankSpacesInSocialSecurityNumber(socialSecurityNumber),

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
            patient.SocialSecurityNumber = RemoveBlankSpacesInSocialSecurityNumber(socialSecurityNumber);

            patient.LastName = lastName;
            patient.FirstName = firstName;
            patient.BirthDate = birthDate;
            patient.BloodGroup = bloodGroup;
            patient.Sex = sex;
            patient.SharedSheet = sharedSheet;

            _patientRepository.UpdatePatient(patient);
        }

        #endregion CRUD Patients

        #region Format social security number

        public string AddBlankSpacesInSocialSecurityNumber(string socialSecurityNumber)
        {
            socialSecurityNumber = Regex.Replace(socialSecurityNumber, @"(\w{1})(\w{2})(\w{2})(\w{2})(\w{3})(\w{3})(\w{2})", @"$1 $2 $3 $4 $5 $6 $7");

            return socialSecurityNumber;
        }

        public string RemoveBlankSpacesInSocialSecurityNumber(string socialSecurityNumber)
        {
            socialSecurityNumber = Regex.Replace(socialSecurityNumber, @"\s", "");

            return socialSecurityNumber;
        }

        #endregion Format social security number
    }
}