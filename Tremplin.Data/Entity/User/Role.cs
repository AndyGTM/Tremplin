using System.ComponentModel.DataAnnotations.Schema;

namespace Tremplin.Data.Entity.User
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}