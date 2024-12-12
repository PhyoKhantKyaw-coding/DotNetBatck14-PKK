using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame;

public class AppDbContent : DbContext
{
    private readonly SqlConnectionStringBuilder _SqlConnectionStringBuilder;
    public AppDbContent()
    {
        _SqlConnectionStringBuilder = new SqlConnectionStringBuilder
        {
            DataSource = ".\\SA",
            InitialCatalog = "SnakesGame",
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
    public DbSet<PlayerModel> player { get; set; }
    public DbSet<GameBoardModel> gameBoard { get; set; }
    public DbSet<GameModel> game { get; set; }
    public DbSet<GameMoveModel> gameMove { get; set; }

}

