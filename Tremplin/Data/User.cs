using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tremplin.Data
{
    [Table("User")]
    public class User
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// User identifier
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Identifiant")]
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}