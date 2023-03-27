using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;

namespace Tremplin.Models.UserViewModels
{
    public class EmailUpdateModel
    {
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("E-mail")]
        [EmailAddress(ErrorMessage = "{0} non valide")]
        [ExistingMail]
        public string Email { get; set; }
    }
}