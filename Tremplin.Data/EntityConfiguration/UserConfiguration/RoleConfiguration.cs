using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tremplin.Data.Entity.User;
using Tremplin.Data.Helpers;

namespace Tremplin.Data.EntityConfiguration.UserConfiguration
{
    public class RoleConfiguration : BaseConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> modelBuilder)
        {
            base.Configure(modelBuilder);
        }
    }

    public class RoleAddConfiguration : BaseAddConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> modelBuilder)
        {
            modelBuilder
                  .Property(p => p.Name)
                  .IsRequired();
        }
    }
}