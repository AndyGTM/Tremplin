using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tremplin.Models
{
    public class ConsultationModel
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        [Required]
        public string ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        [ForeignKey("Patient"), Required]
        public int PatientId { get; set; }
    }
}