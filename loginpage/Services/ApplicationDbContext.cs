using Microsoft.EntityFrameworkCore;
using loginpage.Models;

namespace loginpage.Services
{
    //for database context connected to other files for crud feature
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
