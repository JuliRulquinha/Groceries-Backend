using Microsoft.EntityFrameworkCore;

namespace Groceries
{
    public class MyFirstContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        
        public DbSet<User> Users { get; set; }
            //base.OnConfiguring(optionsBuilder);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
             optionsBuilder.UseSqlServer("Server=localhost;Database=Groceries;Trusted_Connection=True;TrustServerCertificate=True");
        }
    
    }
}
