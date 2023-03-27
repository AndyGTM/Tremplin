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

        [Required(ErrorMessage = "{0} requise")]
        [DisplayName("Date de consultation")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "{0} requise")]
        [DisplayName("Description courte")]
        public string ShortDescription { get; set; }

        [DisplayName("Description longue (optionnelle)")]
        public string? LongDescription { get; set; }

        public int PatientId { get; set; }
    }
}