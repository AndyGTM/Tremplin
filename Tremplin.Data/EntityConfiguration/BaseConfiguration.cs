using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tremplin.Data.Entity;

namespace Tremplin.Data.EntityConfiguration
{
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            modelBuilder.HasKey(n => n.Id);

            modelBuilder.Property(n => n.Id).ValueGeneratedOnAdd();
        }
    }
}