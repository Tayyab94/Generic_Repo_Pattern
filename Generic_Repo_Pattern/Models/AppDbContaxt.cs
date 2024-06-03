using Generic_repo.Models;
using Microsoft.EntityFrameworkCore;

namespace Generic_Repo_Pattern.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
