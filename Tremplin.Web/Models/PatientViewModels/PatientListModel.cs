using System.ComponentModel;
using Tremplin.Core.Enums;

namespace Tremplin.Models.PatientViewModels
{
    public class PatientListModel
    {
        [DisplayName("Numéro de sécurité sociale")]
        public string SocialSecurityNumber { get; set; }

        [DisplayName("Nom")]
        public string LastName { get; set; }

        [DisplayName("Prénom")]
        public string FirstName { get; set; }

        [DisplayName("Date de naissance")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Groupe sanguin")]
        public BloodGroupNames BloodGroup { get; set; }

        [DisplayName("Sexe")]
        public SexTypes Sex { get; set; }

        [DisplayName("Fiche partagée")]
        public bool SharedSheetWithOthersPractitioners { get; set; }

        [DisplayName("Auteur fiche")]
        public bool UserIsCreatorOfPatientSheet { get; set; }

        public List<PatientModel>? Patients { get; set; }

        public string? SearchLastName { get; set; }

        public string? SearchFirstName { get; set; }

        public string? SearchSocialSecurityNumber { get; set; }

        public DateTime? SearchBirthDate { get; set; }
    }
}