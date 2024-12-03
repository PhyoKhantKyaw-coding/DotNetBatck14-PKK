using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp3.EfCoreExamples;

internal class AppDbContext: DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(AppSettings.connectionBuilder.ConnectionString);
    }

    public DbSet<tblBlog> Blogs { get; set; }
}
[Table("tblBlog")]
public class tblBlog
{
    [Key]
    [Column("BlogId")]
    public string Id { get; set; }
    [Column("BlogTitle")]
    public string Title { get; set; }
    [Column("BlogAuthor")]
    public string Author { get; set; }
    [Column("BlogContent")]
    public string Content { get; set; }
}
