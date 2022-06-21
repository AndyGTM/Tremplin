using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models
{
    public class ConsultationDetailsViewModel
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
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
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
        [DisplayName("Description longue (optionnelle)")]
        public string? LongDescription { get; set; }

        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        public int PatientId { get; set; }
    }
}