using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tremplin.Models.ConsultationViewModels
{
    public class ConsultationCreationViewModel
    {
        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        [ForeignKey("Patient"), Required]
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
        [DisplayName("Description courte")]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Long description of the consultation
        /// </summary>
        [DisplayName("Description longue (optionnelle)")]
        public string? LongDescription { get; set; }
    }
}