using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;

namespace Tremplin.Models
{
    public class EmailUpdateViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Adresse e-mail non valide")]
        [ExistingMail]
        public string Email { get; set; }
    }
}