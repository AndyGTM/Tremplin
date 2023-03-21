using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;
using Tremplin.Data.Entity;
using Tremplin.Data.EntityConfiguration;
using Tremplin.Data.Helpers;

namespace Tremplin.Data
{
    public class DataContext : DbContext
    {
        private readonly byte[] _encryptionKey = AesProvider.GenerateKey(AesKeySize.AES256Bits).Key;
        private readonly byte[] _encryptionIV = AesProvider.GenerateKey(AesKeySize.AES256Bits).IV;
        private readonly IEncryptionProvider _provider;

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            _provider = new AesProvider(_encryptionKey, _encryptionIV);
        }

        public DbSet<Patient> Patient { get; set; }

        public DbSet<Consultation> Consultation { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserRole> UserRole { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Tremplin.db;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(_provider);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ConsultationConfiguration()).AddConfiguration(new ConsultationAddConfiguration());

            modelBuilder.ApplyConfiguration(new PatientConfiguration()).AddConfiguration(new PatientAddConfiguration());

            modelBuilder.ApplyConfiguration(new RoleConfiguration()).AddConfiguration(new RoleAddConfiguration());

            modelBuilder.ApplyConfiguration(new UserConfiguration()).AddConfiguration(new UserAddConfiguration());

            modelBuilder.ApplyConfiguration(new UserRoleConfiguration()).AddConfiguration(new UserRoleAddConfiguration());
        }
    }
}