using Microsoft.EntityFrameworkCore;
using Tremplin.Models;

namespace Tremplin.Data
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