using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.Data;
using Tremplin.Enums;

namespace Tremplin.Models
{
    public class PatientListViewModel
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Social security number
        /// </summary>
        [DisplayName("Numéro de sécurité sociale")]
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Patient last name
        /// </summary>
        [DisplayName("Nom")]
        public string LastName { get; set; }

        /// <summary>
        /// Patient first name
        /// </summary>
        [DisplayName("Prénom")]
        public string FirstName { get; set; }

        /// <summary>
        /// Patient birth date
        /// </summary>
        [DisplayName("Date de naissance")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MMMM/yyyy}")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Patient blood group
        /// </summary>
        [DisplayName("Groupe sanguin")]
        public BloodGroupNames BloodGroup { get; set; }

        /// <summary>
        /// Patient sex
        /// </summary>
        [DisplayName("Sexe")]
        public SexTypes Sex { get; set; }

        /// <summary>
        /// Authorize or not the sharing of the patient sheet with others practitioners
        /// </summary>
        [DisplayName("Fiche partagée")]
        public bool SharedSheet { get; set; }

        public List<Patient>? Patients { get; set; }

        /// <summary>
        /// Allow to search patients by last name
        /// </summary>
        public string? SearchLastName { get; set; }

        /// <summary>
        /// Allow to search patients by first name
        /// </summary>
        public string? SearchFirstName { get; set; }

        /// <summary>
        /// Allow to search patients by social security number
        /// </summary>
        public string? SearchSocialSecurityNumber { get; set; }

        /// <summary>
        /// Allow to search patients by birth date
        /// </summary>
        public DateTime? SearchBirthDate { get; set; }
    }
}