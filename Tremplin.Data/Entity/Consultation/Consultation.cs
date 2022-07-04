using System.ComponentModel.DataAnnotations;

namespace Tremplin.Data.Entity.Consultation
{
    public class Consultation : BaseEntity
    {
        /// <summary>
        /// Consultation date
        /// </summary>
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Short description of the consultation
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Long description of the consultation
        /// </summary>
        public string? LongDescription { get; set; }

        /// <summary>
        /// Patient Id associated with this consultation
        /// </summary>
        public int PatientId { get; set; }

        public Patient.Patient Patient { get; set; }
    }
}