using System.ComponentModel;
using Tremplin.Core.Enums;

namespace Tremplin.Models.PatientViewModels
{
    public class PatientListModel
    {
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

        /// <summary>
        /// Check if user is creator of this patient
        /// </summary>
        [DisplayName("Auteur fiche")]
        public bool UserIsCreator { get; set; }

        /// <summary>
        /// List to display patients in the view "Index" (for patients controller)
        /// </summary>
        public List<Patient.PatientModel>? Patients { get; set; }

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