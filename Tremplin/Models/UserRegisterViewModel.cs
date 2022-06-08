﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models
{
    public class UserRegisterViewModel : UserLoginViewModel
    {
        /// <summary>
        /// Password confirmation for users
        /// </summary>
        [Required(ErrorMessage = "Le mot de passe doit être confirmé")]
        [DisplayName("Confirmation du mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmedPassword { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}
