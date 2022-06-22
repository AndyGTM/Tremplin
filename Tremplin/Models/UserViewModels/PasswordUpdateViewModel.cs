using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models.UserViewModels
{
    public class PasswordUpdateViewModel
    {
        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
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
    }
}