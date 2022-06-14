﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.Enums;

namespace Tremplin.Models
{
    public class PatientCreationViewModel
    {
        /// <summary>
        /// Social security number
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Numéro de sécurité sociale")]
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
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Date de naissance")]
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
        public SexTypes Sex { get; set; }

        /// <summary>
        /// Authorize or not the sharing of the patient sheet with others practitioners
        /// </summary>
        [DisplayName("Fiche partagée")]
        public bool SharedSheet { get; set; }
    }
}