using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models.ConsultationViewModels
{
    public class ConsultationUpdateModel
    {
        /// <summary>
        /// Consultation Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Consultation date
        /// </summary>
        [Required(ErrorMessage = "{0} requise")]
        [DisplayName("Date de consultation")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Short description of the consultation
        /// </summary>
        [Required(ErrorMessage = "{0} requise")]
        [DisplayName("Description courte")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Long description of the consultation
        /// </summary>
        [DisplayName("Description longue (optionnelle)")]
        public string? LongDescription { get; set; }

        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        public int PatientId { get; set; }
    }
}