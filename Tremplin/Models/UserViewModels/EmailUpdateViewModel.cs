using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;

namespace Tremplin.Models.UserViewModels
{
    public class EmailUpdateViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "{0} non valide")]
        [ExistingMail]
        public string Email { get; set; }
    }
}