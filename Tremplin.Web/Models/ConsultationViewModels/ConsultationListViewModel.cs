using System.ComponentModel;
using Tremplin.Data;

namespace Tremplin.Models.ConsultationViewModels
{
    public class ConsultationListViewModel
    {
        /// <summary>
        /// Consultation date
        /// </summary>
        [DisplayName("Date de consultation")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Short description of the consultation
        /// </summary>
        [DisplayName("Description")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// List to display consultations in the view "Index" (for consultations controller)
        /// </summary>
        public List<Consultation>? Consultations { get; set; }
    }
}