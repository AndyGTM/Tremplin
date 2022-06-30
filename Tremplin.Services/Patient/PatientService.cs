﻿using Tremplin.Core.Enums;
using Tremplin.Core.Helpers;
using Tremplin.Data;
using Tremplin.IRepositories.IPatient;
using Tremplin.IServices.IPatient;
using Tremplin.Models.Patient;

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

        public PatientModel GetPatientById(int id)
        {
            Patient patient = _patientRepository.GetPatientById(id);

            PatientModel patientModel = MapToPatientModel(patient);

            return patientModel;
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

        public void UpdatePatient(PatientModel patientModel, string socialSecurityNumber, string lastName, string firstName, DateTime birthDate,
            BloodGroupNames bloodGroup, SexTypes sex, bool sharedSheet)
        {
            // Removal of any blank spaces for recording the social security number in the database
            patientModel.SocialSecurityNumber = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(socialSecurityNumber);

            patientModel.LastName = lastName;
            patientModel.FirstName = firstName;
            patientModel.BirthDate = birthDate;
            patientModel.BloodGroup = bloodGroup;
            patientModel.Sex = sex;
            patientModel.SharedSheet = sharedSheet;

            Patient patient = MapToPatient(patientModel);

            _patientRepository.UpdatePatient(patient);
        }

        public void DeletePatient(PatientModel patientModel)
        {
            Patient patient = _patientRepository.GetPatientById(patientModel.Id);

            _patientRepository.DeletePatient(patient);
        }

        #endregion CRUD Patients

        #region Mapping

        /// <summary>
        /// Map PatientModel to Patient
        /// </summary>
        /// <param name="patientModel">Patient model</param>
        /// <returns>Patient entity</returns>
        public Patient MapToPatient(PatientModel patientModel)
        {
            if (patientModel == null)
                return new Patient();

            Patient patient = _patientRepository.GetPatientById(patientModel.Id);

            patient.SocialSecurityNumber = patientModel.SocialSecurityNumber;
            patient.LastName = patientModel.LastName;
            patient.FirstName = patientModel.FirstName;
            patient.BirthDate = patientModel.BirthDate;
            patient.BloodGroup = patientModel.BloodGroup;
            patient.Sex = patientModel.Sex;
            patient.SharedSheet = patientModel.SharedSheet;

            return patient;          
        }

        /// <summary>
        /// Map Patient to PatientModel
        /// </summary>
        /// <param name="patient">Patient entity</param>
        /// <returns>Patient model</returns>
        public static PatientModel MapToPatientModel(Patient patient)
        {
            if (patient == null)
                return new PatientModel();

            return new PatientModel()
            {
                Id = patient.Id,
                SocialSecurityNumber = patient.SocialSecurityNumber,
                LastName = patient.LastName,
                FirstName = patient.FirstName,
                BirthDate = patient.BirthDate,
                BloodGroup = patient.BloodGroup,
                Sex = patient.Sex,
                SharedSheet = patient.SharedSheet,
                CreatedBy = patient.CreatedBy
            };
        }

        #endregion Mapping
    }
}