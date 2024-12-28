using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.RestaurantDataBase
{
    public class AppDbContent : DbContext
    {
        private readonly SqlConnectionStringBuilder _SqlConnectionStringBuilder;
        public AppDbContent() 
        {
            _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = ".\\SA",
                InitialCatalog = "Restaurant",
                UserID = "sa",
                Password = "sa@123",
                TrustServerCertificate = true,
            };
              
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_SqlConnectionStringBuilder.ConnectionString);
            }
        }

        public DbSet<MenuItem> Mitem { get; set; }
        public DbSet<Order> Order { get; set; }    
        public DbSet<OrderItem> OrderItem { get; set; }
    }
}
