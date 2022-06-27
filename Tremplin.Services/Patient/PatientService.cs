using System.Text.RegularExpressions;
using Tremplin.Core.Enums;
using Tremplin.Data;
using Tremplin.IServices.IPatient;

namespace Tremplin.Services
{
    public class PatientService : IPatientService
    {
        private DataContext DataContext { get; init; }

        public PatientService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        /// <summary>
        /// Gets list of patients with shared sheet and/or created by logged user
        /// </summary>
        /// <param name="user">Logged user</param>
        public IQueryable<Patient> GetPatients(User user)
        {
            IQueryable<Patient> patients = from m in DataContext.Patients
                                           where m.SharedSheet || m.CreatedBy == user.UserName
                                           select m;
            return patients;
        }

        /// <summary>
        /// Creation of a patient by logged user
        /// </summary>
        /// <param name="userName">Logged user</param>
        public Patient CreatePatient(string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet, string userName)
        {
            Patient patient = new()
            {
                // Removal of any blank spaces for recording the social security number in the database
                SocialSecurityNumber = Regex.Replace(socialSecurityNumber, @"\s", ""),

                LastName = lastName,
                FirstName = firstName,
                BirthDate = birthDate,
                BloodGroup = bloodGroup,
                Sex = sex,
                SharedSheet = sharedSheet,
                CreatedBy = userName
            };

            return patient;
        }

        public Patient UpdatePatient(Patient patient, string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet)
        {
            // Removal of any blank spaces for recording the social security number in the database
            patient.SocialSecurityNumber = Regex.Replace(socialSecurityNumber, @"\s", "");

            patient.LastName = lastName;
            patient.FirstName = firstName;
            patient.BirthDate = birthDate;
            patient.BloodGroup = bloodGroup;
            patient.Sex = sex;
            patient.SharedSheet = sharedSheet;

            return patient;
        }
    }
}