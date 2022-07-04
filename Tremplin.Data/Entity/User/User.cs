using System.ComponentModel.DataAnnotations;

namespace Tremplin.Data.Entity
{
    public class User : BaseEntity
    {
        /// <summary>
        /// User identifier
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public string Email { get; set; }
    }
}