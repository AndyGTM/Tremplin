using Microsoft.EntityFrameworkCore;

namespace Tremplin.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<HealthProfessional> HealthProfessionals { get; set; }
    }
}