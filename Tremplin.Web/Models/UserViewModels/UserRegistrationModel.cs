using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;

namespace Tremplin.Models.UserViewModels
{
    public class UserRegistrationModel
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Identifiant")]
        [ExistingUserName]
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

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
        [Required(ErrorMessage = "{0} requis")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "{0} non valide")]
        [ExistingMail]
        public string Email { get; set; }
    }
}