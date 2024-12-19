using DotNetBatch14PKK.MinimalApi2;
using DotNetBatch14PKK.MinimalApi2.Features.Emoji;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.MinimalApi2;

public class AppDbContent : DbContext
{
    private readonly SqlConnectionStringBuilder _SqlConnectionStringBuilder;
    public AppDbContent() 
    {
        _SqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".\\SA",
            InitialCatalog = "Emoji",
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

    public DbSet<EmojiModel> EmojiModel { get; set; }
}
