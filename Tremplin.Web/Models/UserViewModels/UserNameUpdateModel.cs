using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;

namespace Tremplin.Models.UserViewModels
{
    public class UserNameUpdateModel
    {
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Identifiant")]
        [ExistingUserName]
        public string UserName { get; set; }
    }
}