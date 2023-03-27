using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tremplin.Models.ConsultationViewModels
{
    public class ConsultationCreationModel
    {
        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        [ForeignKey("Patient"), Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} requise")]
        [DisplayName("Date de consultation")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "{0} requise")]
        [DisplayName("Description courte")]
        public string ShortDescription { get; set; }

        [DisplayName("Description longue (optionnelle)")]
        public string? LongDescription { get; set; }
    }
}