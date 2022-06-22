using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tremplin.Enums;

namespace Tremplin.Data
{
    [Table("Patient")]
    public class Patient
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Social security number
        /// </summary>
        [Required]
        [Column(TypeName="char")]
        [StringLength(15)]
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Patient last name
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Patient first name
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Patient birth date
        /// </summary>
        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Patient blood group
        /// </summary>
        [Required]
        public BloodGroupNames BloodGroup { get; set; }

        /// <summary>
        /// Patient sex
        /// </summary>
        [Required]
        [Column(TypeName = "tinyint")]
        public SexTypes Sex { get; set; }

        /// <summary>
        /// Authorize or not the sharing of the patient sheet with others practitioners
        /// </summary>
        public bool SharedSheet { get; set; }

        /// <summary>
        /// User who created this patient
        /// </summary>
        public string CreatedBy { get; set; }
    }
}