using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models.UserViewModels
{
    public class UserLoginModel
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Identifiant")]
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Remember user
        /// </summary>
        [DisplayName("Se souvenir de moi")]
        public bool IsRememberMe { get; set; }
    }
}