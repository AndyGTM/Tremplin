using System.ComponentModel.DataAnnotations;
using Tremplin.Core.Enums;

namespace Tremplin.Models.Patient
{
    public class PatientModel
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
        public SexTypes Sex { get; set; }

        /// <summary>
        /// Authorize or not the sharing of the patient sheet with others practitioners
        /// </summary>
        public bool SharedSheet { get; set; }

        /// <summary>
        /// User who created this patient
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Check if user is creator of this patient
        /// </summary>
        public bool UserIsCreator { get; set; }
    }
}