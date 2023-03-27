using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.Core.Enums;

namespace Tremplin.Models.PatientViewModels
{
    public class PatientDeleteModel
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        [DisplayName("Numéro de sécurité sociale")]
        public string SocialSecurityNumber { get; set; }

        [DisplayName("Nom")]
        public string LastName { get; set; }

        [DisplayName("Prénom")]
        public string FirstName { get; set; }

        [DisplayName("Date de naissance")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Groupe sanguin")]
        public BloodGroupNames BloodGroup { get; set; }

        [DisplayName("Sexe")]
        public SexTypes Sex { get; set; }

        [DisplayName("Fiche partagée")]
        public bool SharedSheetWithOthersPractitioners { get; set; }
    }
}