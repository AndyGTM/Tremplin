using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tremplin.CustomValidation;

namespace Tremplin.Models.UserViewModels
{
    public class UserNameUpdateViewModel
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [Required(ErrorMessage = "{0} requis")]
        [DisplayName("Identifiant")]
        [ExistingUserName]
        public string UserName { get; set; }
    }
}