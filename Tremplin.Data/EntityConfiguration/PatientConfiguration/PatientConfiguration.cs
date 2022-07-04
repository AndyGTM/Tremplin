using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tremplin.Data.Entity.Patient;
using Tremplin.Data.Helpers;

namespace Tremplin.Data.EntityConfiguration.PatientConfiguration
{
    public class PatientConfiguration : BaseConfiguration<Patient>
    {
        public override void Configure(EntityTypeBuilder<Patient> modelBuilder)
        {
            base.Configure(modelBuilder);
        }
    }

    public class PatientAddConfiguration : BaseAddConfiguration<Patient>
    {
        public override void Configure(EntityTypeBuilder<Patient> modelBuilder)
        {
            modelBuilder
                  .Property(p => p.SocialSecurityNumber)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.LastName)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.FirstName)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.BirthDate)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.BloodGroup)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.Sex)
                  .IsRequired()
                  .HasColumnType("tinyint");
        }
    }
}