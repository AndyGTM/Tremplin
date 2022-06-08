using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tremplin.Data;

[Table("Role")]
public class Role
{
    [Key, Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }
}