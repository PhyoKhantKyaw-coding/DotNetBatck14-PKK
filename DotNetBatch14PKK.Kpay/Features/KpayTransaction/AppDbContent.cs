using DotNetBatch14PKK.Kpay;
using DotNetBatch14PKK.Kpay.Features.KpayTransaction;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Batch14PKK.Kpay.Features.Users;

public class AppDbContent : DbContext
{
    private readonly SqlConnectionStringBuilder _SqlConnectionStringBuilder;
    public AppDbContent() 
    {
        _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".\\SA",
            InitialCatalog = "Kpay",
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

    public DbSet<UserModel> users { get; set; }
    public DbSet<TranModel> trans { get; set; }
}
