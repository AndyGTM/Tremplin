using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.Core.Enums;
using Tremplin.CustomValidation;

namespace Tremplin.Models.PatientViewModels
{
    public class PatientCreationModel
    {
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Numéro de sécurité sociale")]
        [RegularExpression(@"[0-9]{1} [0-9]{2} [0-9]{2} [0-9]{2} [0-9]{3} [0-9]{3} [0-9]{2}|[0-9]{15}",
            ErrorMessage = @"Le {0} doit être composé de chiffres au format ""x xx xx xx xxx xxx xx"" ou ""xxxxxxxxxxxxxxx""")]
        [ExistingSocialSecurityNumber]
        public string SocialSecurityNumber { get; set; }

        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Nom")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Prénom")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} requise")]
        [DisplayName("Date de naissance")]
        [BirthDateBeforeOrEqualToToday(ErrorMessage = "La {0} ne peut pas être après la date d'aujourd'hui")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Groupe sanguin")]
        public BloodGroupNames BloodGroup { get; set; }

        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Sexe")]
        public SexTypes Sex { get; set; }

        [DisplayName("Fiche partagée")]
        public bool SharedSheetWithOthersPractitioners { get; set; }
    }
}