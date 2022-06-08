using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tremplin.Models
{
    [Table("HealthProfessional")]
    public class HealthProfessional
    {
        /// <summary>
        /// Health professional Id
        /// </summary>
        [Key, Required]
        public int Id { get; set; }

        /// <summary>
        /// Health professional identifier
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Identifiant")]
        public string UserName { get; set; }

        /// <summary>
        /// Health professional password
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "Mot de passe")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation for health professionals
        /// </summary>
        [Required(ErrorMessage = "Le mot de passe doit être confirmé")]
        [Display(Name = "Confirmation du mot de passe")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas")]
        [NotMapped]
        public string ConfirmedPassword { get; set; }

        /// <summary>
        /// Health professional email
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
    }
}