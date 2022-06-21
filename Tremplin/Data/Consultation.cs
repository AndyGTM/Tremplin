using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tremplin.Data
{
    [Table("Consultation")]
    public class Consultation
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
        public DateTime Date { get; set; }

        /// <summary>
        /// Short description of the consultation
        /// </summary>
        [Required]
        public string ShortDescription { get; set; }

        /// <summary>
        /// Long description of the consultation
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        [ForeignKey("Patient"), Required]
        public int PatientId { get; set; }

        /// <summary>
        /// Patient associated with this consultation
        /// </summary>
        public Patient Patient { get; set; }
    }
}