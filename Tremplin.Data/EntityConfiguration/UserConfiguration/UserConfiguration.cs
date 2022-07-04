using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tremplin.Data.Entity;
using Tremplin.Data.Helpers;

namespace Tremplin.Data.EntityConfiguration.UserConfiguration
{
    public class UserConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            base.Configure(modelBuilder);
        }
    }

    public class UserAddConfiguration : BaseAddConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder
                  .Property(p => p.UserName)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.Password)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.Email)
                  .IsRequired();
        }
    }
}