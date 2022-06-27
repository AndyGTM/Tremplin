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
        /// <param name="socialSecurityNumber">Patient social security number</param>
        /// <param name="lastName">Patient last name</param>
        /// <param name="firstName">Patient first name</param>
        /// <param name="birthDate">Patient birth date</param>
        /// <param name="bloodGroup">Patient blood group</param>
        /// <param name="sex">Patient sex</param>
        /// <param name="sharedSheet">Authorize or not the sharing of the patient sheet with others practitioners</param>
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
    }
}