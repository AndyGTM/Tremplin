using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Tremplin.Data.Entity.Consultation;
using Tremplin.Data.Entity;
using Tremplin.Data.Entity.User;
using Tremplin.Data.EntityConfiguration.ConsultationConfiguration;
using Tremplin.Data.EntityConfiguration.PatientConfiguration;
using Tremplin.Data.EntityConfiguration.UserConfiguration;
using Tremplin.Data.Helpers;

namespace Tremplin.Data
{
    public class DataContext : DbContext
    {
        // Get key and IV from a Base64String or any other ways
        private readonly byte[] _encryptionKey = AesProvider.GenerateKey(AesKeySize.AES256Bits).Key;
        private readonly byte[] _encryptionIV = AesProvider.GenerateKey(AesKeySize.AES256Bits).IV;
        private readonly IEncryptionProvider _provider;

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            this._provider = new AesProvider(this._encryptionKey, this._encryptionIV);
        }

        public DbSet<Patient> Patient { get; set; }

        public DbSet<Consultation> Consultation { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Tremplin.db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);

            base.OnModelCreating(modelBuilder);

            // Entity declaration for Code First
            modelBuilder.ApplyConfiguration(new ConsultationConfiguration()).AddConfiguration(new ConsultationAddConfiguration());

            modelBuilder.ApplyConfiguration(new PatientConfiguration()).AddConfiguration(new PatientAddConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration()).AddConfiguration(new RoleAddConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration()).AddConfiguration(new UserAddConfiguration());

            modelBuilder.ApplyConfiguration(new UserRoleConfiguration()).AddConfiguration(new UserRoleAddConfiguration());
        }
    }
}