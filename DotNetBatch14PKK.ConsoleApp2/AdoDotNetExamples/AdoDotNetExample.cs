using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp2.AotDotNetExamples;

internal class AdoDotNetExample
{
    private readonly SqlConnectionStringBuilder _connectionBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = ".\\SA",
        InitialCatalog = "Blog",
        UserID = "sa",
        Password = "sa@123",
        TrustServerCertificate = true,
    };
    public void Read()
    {
        SqlConnection connection = new SqlConnection(_connectionBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("Select * from tblBlog", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        connection.Close();
        foreach (DataRow dr in dt.Rows)
        {
            Console.WriteLine("id = " + dr["BlogId"]);
            Console.WriteLine("title = " + dr["BlogTitle"]);
            Console.WriteLine("author = " + dr["BlogAuthor"]);
            Console.WriteLine("content = " + dr["BlogContent"]);
            Console.WriteLine("");
        }
    }
    public void Edit(string id)
    {
        SqlConnection connection = new SqlConnection(_connectionBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand($"select * from tblBlog where BlogId = '{id}'", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adapter.Fill(dt);
        connection.Close();
        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No data found.");
            return;
        }

        DataRow row = dt.Rows[0];
        Console.WriteLine(row["BlogId"]);
        Console.WriteLine(row["BlogTitle"]);
        Console.WriteLine(row["BlogAuthor"]);
        Console.WriteLine(row["BlogContent"]);
    }
    public void create(string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(_connectionBuilder.ConnectionString);
        connection.Open();
        string query = $@"INSERT INTO [dbo].[tblBlog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('{title}'
           ,'{author}'
           ,'{content}')";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        int result = cmd.ExecuteNonQuery();
        string message = result > 0 ? "Saving successful." : "Saving failed.";
        Console.WriteLine(message);
        connection.Close();
    }

    public void update(string id, string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(_connectionBuilder.ConnectionString);
        connection.Open();
        DataTable dt = new DataTable();
        string query = $@"UPDATE [dbo].[tblBlog]
           SET [BlogTitle] = '{title}'
              ,[BlogAuthor] = '{author}'
              ,[BlogContent] = '{content}'
                WHERE BlogId = '{id}'";
        SqlCommand cmd = new(query, connection);
        int result = cmd.ExecuteNonQuery();
        connection.Close();
        string message = result > 0 ? "Updating successful." : "Updating failed.";
        Console.WriteLine(message);
    }


    public void Delete(string id)
    {
        SqlConnection connection = new SqlConnection(_connectionBuilder.ConnectionString);
        connection.Open();
        SqlCommand cmd = new($"delete from tblBlog where BlogId = '{id}'", connection);
        int result = cmd.ExecuteNonQuery();
        connection.Close();
        string message = result > 0 ? "Deleting successful." : "Deleting failed.";
        Console.WriteLine(message);
    }

}
