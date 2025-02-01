using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.Login.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserModel>()
            .Property(u => u.UserID)
            .HasColumnType("uniqueidentifier"); // Ensure this matches the database column type
    }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<FeatureModel> Features { get; set; }
    public DbSet<RoleModel> Role { get; set; }
    public DbSet<UserPermissionModel> UserPermission { get; set; }
}
