using Microsoft.EntityFrameworkCore;
namespace BlazorSolution.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
