﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;
using Tremplin.Enums;

namespace Tremplin.Models
{
    public class PatientUpdateViewModel
    {
        /// <summary>
        /// Patient Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Social security number
        /// </summary>
        [Required(ErrorMessage = "Le {0} est requis")]
        [DisplayName("Numéro de sécurité sociale")]
        [RegularExpression(@"[0-9]{1} [0-9]{2} [0-9]{2} [0-9]{2} [0-9]{3} [0-9]{3} [0-9]{2}|[0-9]{15}",
            ErrorMessage = @"Le {0} doit être composé de chiffres au format ""x xx xx xx xxx xxx xx"" ou ""xxxxxxxxxxxxxxx""")]
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
        [BirthDateNotAfterToday(ErrorMessage = "La {0} ne peut pas être après la date d'aujourd'hui")]
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