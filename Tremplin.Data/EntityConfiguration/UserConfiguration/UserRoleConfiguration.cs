using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tremplin.Data.Entity;
using Tremplin.Data.Helpers;

namespace Tremplin.Data.EntityConfiguration.UserConfiguration
{
    public class UserRoleConfiguration : BaseConfiguration<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> modelBuilder)
        {
            base.Configure(modelBuilder);

            modelBuilder
                .HasOne(s => s.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(c => c.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            modelBuilder
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }

    public class UserRoleAddConfiguration : BaseAddConfiguration<UserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> modelBuilder)
        {

        }
    }
}
