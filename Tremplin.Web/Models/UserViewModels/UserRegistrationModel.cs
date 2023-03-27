﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;

namespace Tremplin.Models.UserViewModels
{
    public class UserRegistrationModel
    {
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Identifiant")]
        [ExistingUserName]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être confirmé")]
        [DisplayName("Confirmation du mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmedPassword { get; set; }

        [Required(ErrorMessage = "{0} requis")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "{0} non valide")]
        [ExistingMail]
        public string Email { get; set; }
    }
}