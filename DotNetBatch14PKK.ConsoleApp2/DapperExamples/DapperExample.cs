using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DotNetBatch14PKK.ConsoleApp2.Dtos;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DotNetBatch14PKK.ConsoleApp2.DapperExamples;

internal class DapperExample
{
    private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;
    public void Read()
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        var blogs = connection.Query<BlogDtos>("select * from tblBlog").ToList();
        foreach (var blog in blogs)
        {
            Console.WriteLine(blog.BlogId);
            Console.WriteLine(blog.BlogTitle);
            Console.WriteLine(blog.BlogAuthor);
            Console.WriteLine(blog.BlogContent);
        }
    }

    public void Edit(string id)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        var blog = connection.Query<BlogDtos>($"select * from tblBlog where BlogId = '{id}'").FirstOrDefault();
        if (blog is null)
        {
            Console.WriteLine("No data found.");
            return;
        }
        Console.WriteLine(blog.BlogId);
        Console.WriteLine(blog.BlogTitle);
        Console.WriteLine(blog.BlogAuthor);
        Console.WriteLine(blog.BlogContent);
    }

    public void Create(string title, string author, string content)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string query = $@"INSERT INTO [dbo].[tblBlog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('{title}'
           ,'{author}'
           ,'{content}')";

        int result = connection.Execute(query);
        string message = result > 0 ? "Saving successful." : "Saving failed.";
        Console.WriteLine(message);
    }
    public void Update(string id, string title, string author, string content)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        string query = $@"UPDATE [dbo].[tblBlog]
           SET [BlogTitle] = '{title}'
              ,[BlogAuthor] = '{author}'
              ,[BlogContent] = '{content}'
         WHERE BlogId = '{id}'";
        int result = connection.Execute(query);
        string message = result > 0 ? "Updating successful." : "Updating failed.";
        Console.WriteLine(message);
    }

    public void Delete(string id)
    {
        using IDbConnection connection = new SqlConnection(_connectionString);
        int result = connection.Execute($"delete from tblBlog where BlogId = '{id}'");
        string message = result > 0 ? "Deleting successful." : "Deleting failed.";
        Console.WriteLine(message);
    }
}

