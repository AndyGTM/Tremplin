using Tremplin.Data;
using Tremplin.IServices.IConsultation;

namespace Tremplin.Services
{
    public class ConsultationService : IConsultationService
    {
        private DataContext DataContext { get; init; }

        public ConsultationService(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        /// <summary>
        /// Gets list of consultations by patient Id
        /// </summary>
        public IQueryable<Consultation> GetConsultations(int idPatient)
        {
            IQueryable<Consultation> consultations = from m in DataContext.Consultations
                                                     where m.PatientId == idPatient
                                                     select m;
            return consultations;
        }
    }
}