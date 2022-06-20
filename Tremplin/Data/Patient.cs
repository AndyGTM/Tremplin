using System.ComponentModel;
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
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Numéro de sécurité sociale")]
        [Column(TypeName="char")]
        [StringLength(15)]
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Patient last name
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Nom")]
        public string LastName { get; set; }

        /// <summary>
        /// Patient first name
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Prénom")]
        public string FirstName { get; set; }

        /// <summary>
        /// Patient birth date
        /// </summary>
        [Required(ErrorMessage = "La {0} est requise")]
        [DisplayName("Date de naissance")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Patient blood group
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Groupe sanguin")]
        public BloodGroupNames BloodGroup { get; set; }

        /// <summary>
        /// Patient sex
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Sexe")]
        [Column(TypeName = "tinyint")]
        public SexTypes Sex { get; set; }

        /// <summary>
        /// Authorize or not the sharing of the patient sheet with others practitioners
        /// </summary>
        [DisplayName("Fiche partagée")]
        public bool SharedSheet { get; set; }

        /// <summary>
        /// User who created this patient
        /// </summary>
        [DisplayName("Créé par")]
        public string CreatedBy { get; set; }
    }
}