using DotNetBatch14PKK.Mini_Pos.Features.MiniPos;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.Mini_Pos.Features.MiniPos;

public class AppDbContent : DbContext
{
    private readonly SqlConnectionStringBuilder _SqlConnectionStringBuilder;
    public AppDbContent() 
    {
        _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".\\SA",
            InitialCatalog = "MiniPos",
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

    public DbSet<CategoryModel> cats { get; set; }
    public DbSet<ProductModel> product { get; set; }
    public DbSet<SaleModel> sale { get; set; }
    public DbSet<SaleDetailModel> saledetail { get; set; }

}
