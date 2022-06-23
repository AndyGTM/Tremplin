using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.DataEncryption;
using Microsoft.EntityFrameworkCore.DataEncryption.Providers;

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

        public DbSet<Patient> Patients { get; set; }

        public DbSet<Consultation> Consultations { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseEncryption(this._provider);
        }
    }
}