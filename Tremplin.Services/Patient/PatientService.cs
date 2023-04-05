using Tremplin.Core.Helpers;
using Tremplin.Data.Entity;
using Tremplin.IRepositories;
using Tremplin.IServices;
using Tremplin.Models;

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

        public PatientModel GetPatientById(int patientId)
        {
            Patient patientEntity = _patientRepository.GetPatientById(patientId);

            PatientModel patientModel = MapPatientEntityToPatientModel(patientEntity);

            return patientModel;
        }

        public IEnumerable<PatientModel> GetPatients(string userName)
        {
            IQueryable<Patient> patients = _patientRepository.GetPatients().Where(m => m.SharedSheet || m.CreatedBy == userName);

            List<PatientModel> patientsModels = new();

            foreach (Patient patientEntity in patients)
            {
                PatientModel patientModel = MapPatientEntityToPatientModel(patientEntity);
                patientsModels.Add(patientModel);
            }

            return patientsModels;
        }

        public void CreatePatient(PatientModel patientModel)
        {
            Patient patientEntity = MapPatientModelToPatientEntity(patientModel);

            _patientRepository.CreatePatient(patientEntity);
        }

        public void UpdatePatient(PatientModel patientModel)
        {
            Patient patientEntity = MapPatientModelToPatientEntity(patientModel);

            _patientRepository.UpdatePatient(patientEntity);
        }

        public void DeletePatient(PatientModel patientModel)
        {
            Patient patientEntity = _patientRepository.GetPatientById(patientModel.Id);

            _patientRepository.DeletePatient(patientEntity);
        }

        #endregion CRUD Patients

        #region Mapping

        public Patient MapPatientModelToPatientEntity(PatientModel patientModel)
        {
            if (patientModel == null)
                return new Patient();

            Patient patientEntity = _patientRepository.GetPatientById(patientModel.Id);

            if (patientEntity == null)
            {
                Patient newPatient = new()
                {
                    SocialSecurityNumber = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber),
                    LastName = patientModel.LastName,
                    FirstName = patientModel.FirstName,
                    BirthDate = patientModel.BirthDate,
                    BloodGroup = patientModel.BloodGroup,
                    Sex = patientModel.Sex,
                    SharedSheet = patientModel.SharedSheetWithOthersPractitioners,
                    CreatedBy = patientModel.CreatedBy
                };

                return newPatient;
            }

            patientEntity.SocialSecurityNumber = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber);
            patientEntity.LastName = patientModel.LastName;
            patientEntity.FirstName = patientModel.FirstName;
            patientEntity.BirthDate = patientModel.BirthDate;
            patientEntity.BloodGroup = patientModel.BloodGroup;
            patientEntity.Sex = patientModel.Sex;
            patientEntity.SharedSheet = patientModel.SharedSheetWithOthersPractitioners;

            return patientEntity;
        }

        public static PatientModel MapPatientEntityToPatientModel(Patient patientEntity)
        {
            if (patientEntity == null)
                return new PatientModel();

            return new PatientModel()
            {
                Id = patientEntity.Id,
                SocialSecurityNumber = patientEntity.SocialSecurityNumber,
                LastName = patientEntity.LastName,
                FirstName = patientEntity.FirstName,
                BirthDate = patientEntity.BirthDate,
                BloodGroup = patientEntity.BloodGroup,
                Sex = patientEntity.Sex,
                SharedSheetWithOthersPractitioners = patientEntity.SharedSheet,
                CreatedBy = patientEntity.CreatedBy
            };
        }

        #endregion Mapping
    }
}