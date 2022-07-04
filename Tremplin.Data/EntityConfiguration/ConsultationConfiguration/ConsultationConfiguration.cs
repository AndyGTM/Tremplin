using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tremplin.Data.Entity;
using Tremplin.Data.Helpers;

namespace Tremplin.Data.EntityConfiguration.ConsultationConfiguration
{
    public class ConsultationConfiguration : BaseConfiguration<Consultation>
    {
        public override void Configure(EntityTypeBuilder<Consultation> modelBuilder)
        {
            base.Configure(modelBuilder);

            modelBuilder
                .HasOne(s => s.Patient)
                .WithMany(c => c.Consultations)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }

    public class ConsultationAddConfiguration : BaseAddConfiguration<Consultation>
    {
        public override void Configure(EntityTypeBuilder<Consultation> modelBuilder)
        {
            modelBuilder
                  .Property(p => p.Date)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.ShortDescription)
                  .IsRequired();

            modelBuilder
                  .Property(p => p.PatientId)
                  .IsRequired();
        }
    }
}