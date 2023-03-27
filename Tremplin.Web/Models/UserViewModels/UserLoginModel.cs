using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models.UserViewModels
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Identifiant")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Se souvenir de moi")]
        public bool IsRememberMe { get; set; }
    }
}