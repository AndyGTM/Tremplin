using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models
{
    public class UserNameUpdateViewModel
    {
        /// <summary>
        /// User identifier
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("Identifiant")]
        public string UserName { get; set; }
    }
}