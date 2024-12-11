using DotNetBatch14PKK.Share;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Batch14PKK.Share;

public class AppDbContent : DbContext
{
    private readonly SqlConnectionStringBuilder _SqlConnectionStringBuilder;
    public AppDbContent() 
    {
        _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".\\SA",
            InitialCatalog = "Blog",
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

    public DbSet<BlogModel> Blogs { get; set; }
}
