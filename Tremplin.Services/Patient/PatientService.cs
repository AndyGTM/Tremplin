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
    }
}