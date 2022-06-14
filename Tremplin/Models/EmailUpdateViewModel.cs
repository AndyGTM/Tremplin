using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models
{
    public class EmailUpdateViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "L'{0} est requis")]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "Adresse e-mail non valide")]
        public string Email { get; set; }
    }
}