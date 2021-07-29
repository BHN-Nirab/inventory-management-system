using Microsoft.EntityFrameworkCore;

namespace ServerApp.Model
{
    public class DatabaseContext: DbContext
    {
        private const string ConnectionString = @"Server=NIRAB\MSSQLSERVER03;Database=InventoryManagementSystem;Trusted_Connection=true";

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
