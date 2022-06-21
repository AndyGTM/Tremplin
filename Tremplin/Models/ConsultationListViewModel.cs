using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.Data;

namespace Tremplin.Models
{
    public class ConsultationListViewModel
    {
        /// <summary>
        /// Consultation Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Consultation date
        /// </summary>
        [Required]
        [DisplayName("Date de consultation")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Short description of the consultation
        /// </summary>
        [Required]
        [DisplayName("Description")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Long description of the consultation
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        [Required]
        public int PatientId { get; set; }

        /// <summary>
        /// Patient associated with this consultation
        /// </summary>
        public Patient Patient { get; set; }

        public List<Consultation>? Consultations { get; set; }
    }
}