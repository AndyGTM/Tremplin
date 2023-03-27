using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models.ConsultationViewModels
{
    public class ConsultationDetailsModel
    {
        /// <summary>
        /// Consultation Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        [DisplayName("Date de consultation")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [DisplayName("Description")]
        public string ShortDescription { get; set; }

        [DisplayName("Description longue")]
        public string? LongDescription { get; set; }

        public int PatientId { get; set; }
    }
}