using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models
{
    public class UserRegistrationViewModel : UserLoginViewModel
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
        [Required(ErrorMessage = "L'{0} est requis")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Adresse e-mail non valide")]
        public string Email { get; set; }
    }
}