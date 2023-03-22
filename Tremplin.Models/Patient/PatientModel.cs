using System.ComponentModel.DataAnnotations;
using Tremplin.Core.Enums;

namespace Tremplin.Models
{
    public class PatientModel
    {
        [Key, Required]
        public int Id { get; set; }

        [Required]
        public string SocialSecurityNumber { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        public BloodGroupNames BloodGroup { get; set; }

        [Required]
        public SexTypes Sex { get; set; }

        public bool SharedSheet { get; set; }

        public string CreatedBy { get; set; }

        public bool UserIsCreatorOfPatientSheet { get; set; }
    }
}