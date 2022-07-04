using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tremplin.Data.Entity;

namespace Tremplin.Data.EntityConfiguration
{
    /// <summary>
    /// Default configuration for TEntity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> modelBuilder)
        {
            // Key
            modelBuilder.HasKey(n => n.Id);

            // Auto-increment
            modelBuilder.Property(n => n.Id).ValueGeneratedOnAdd();
        }
    }
}