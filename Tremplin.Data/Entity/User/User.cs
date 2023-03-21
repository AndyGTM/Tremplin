using System.ComponentModel.DataAnnotations;

namespace Tremplin.Data.Entity
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string Email { get; set; }
    }
}