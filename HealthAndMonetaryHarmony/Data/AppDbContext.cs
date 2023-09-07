using HealthAndMonetaryHarmony.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthAndMonetaryHarmony.Data
{
    

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
       
    }

}
