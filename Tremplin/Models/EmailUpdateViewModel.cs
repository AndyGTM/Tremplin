using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tremplin.Models
{
    public class EmailUpdateViewModel
    {
        /// <summary>
        /// User email
        /// </summary>
        [Required(ErrorMessage = "Le champ {0} est requis")]
        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}