using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace DotNet_Batch14PKK.BlogShare;

public class AppDbContent : DbContext
{
    public AppDbContent(DbContextOptions<AppDbContent> options) : base(options)
    {
    }
    public DbSet<BlogModel> Blogs { get; set; }
}
