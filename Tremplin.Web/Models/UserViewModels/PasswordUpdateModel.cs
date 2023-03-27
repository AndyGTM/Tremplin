using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models.UserViewModels
{
    public class PasswordUpdateModel
    {
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Le mot de passe doit être confirmé")]
        [DisplayName("Confirmation du mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        public string ConfirmedPassword { get; set; }
    }
}