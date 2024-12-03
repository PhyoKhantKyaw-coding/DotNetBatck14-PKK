using DotNetBatch14PKK.RestApi;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNet_Batch14PKK.RestApi.Features.Blog;

public class BlogServices : IBlogServices
{
    private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;
    public List<BlogModel> GetBlogs()
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new SqlCommand("select * from tblBlog", connection);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new();
        adapter.Fill(dt);
        connection.Close();

        var list = new List<BlogModel>();
        foreach (DataRow drow in dt.Rows)
        {
            BlogModel blog = new()
            {
                BlogId = drow["BlogId"].ToString(),
                BlogTitle = drow["BlogTitle"].ToString(),
                BlogAuthor = drow["BlogAuthor"].ToString(),
                BlogContent = drow["BlogContent"].ToString()
            };
            list.Add(blog);
        }
        return list;
    }
    public BlogModel GetBlog(string id)
    {

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        SqlCommand cmd = new("select * from tblBlog where BlogId = @BlogId", connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable dt = new();
        adapter.Fill(dt);
        connection.Close();

        if (dt.Rows.Count == 0)
        {
            Console.WriteLine("No data found!");
            return null;
        }
        DataRow drow = dt.Rows[0];
        BlogModel blog = new BlogModel()
        {
            BlogId = drow["BlogId"].ToString(),
            BlogTitle = drow["BlogTitle"].ToString(),
            BlogAuthor = drow["BlogAuthor"].ToString(),
            BlogContent = drow["BlogContent"].ToString()
        };
        return blog;
    }
    public ResponseModel CreateBlog(BlogModel requestModel)
    {
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        string query = @"INSERT INTO [dbo].[tblBlog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent);";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Saving successful." : "Saving failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
    public ResponseModel UpdateBlog(BlogModel requestModel)
    {
        var blog = GetBlog(requestModel.BlogId!);
        if (blog is null) return new()
        {
            IsSuccessful = false,
            Message = "No data found."
        };

        if (String.IsNullOrEmpty(requestModel.BlogTitle))
        {
            requestModel.BlogTitle = blog.BlogTitle;
        }
        else if (String.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            requestModel.BlogAuthor = blog.BlogAuthor;
        }
        else if (String.IsNullOrEmpty(requestModel.BlogContent))
        {
            requestModel.BlogContent = blog.BlogContent;
        }

        SqlConnection connection = new(_connectionString);
        connection.Open();
        string query = $@"UPDATE [dbo].[tblBlog]
           SET [BlogTitle] = @BlogTitle
              ,[BlogAuthor] = @BlogAuthor
              ,[BlogContent] = @BlogContent
         WHERE BlogId = @BlogId";
        SqlCommand cmd = new(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
        cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
        cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
        cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
        int result = cmd.ExecuteNonQuery();
        connection.Close();

        string message = result > 0 ? "Updating successful." : "Updating failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
    public ResponseModel UpsertBlog(BlogModel requestModel)
    {
        var responseModel = new ResponseModel();
        var blog = GetBlog(requestModel.BlogId!);

        if (blog is not null)
        {
            SqlConnection connection = new(_connectionString);
            connection.Open();
            string query = $@"UPDATE [dbo].[tblBlog]
               SET [BlogTitle] = @BlogTitle
                  ,[BlogAuthor] = @BlogAuthor
                  ,[BlogContent] = @BlogContent
             WHERE BlogId = @BlogId";
            SqlCommand cmd = new(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
            cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
            int result = cmd.ExecuteNonQuery();
            connection.Close();

            string message = result > 0 ? "Updating successful." : "Updating failed.";
            responseModel.IsSuccessful = result > 0;
            responseModel.Message = message;
        }

        else if (blog is null)
        {
            responseModel = CreateBlog(requestModel);
        }
        return responseModel;
    }

    public ResponseModel DeleteBlog(string id)
    {
        SqlConnection connection = new(_connectionString);
        connection.Open();
        SqlCommand cmd = new("delete from tblBlog where BlogId = @BlogId", connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        int result = cmd.ExecuteNonQuery();
        connection.Close();
        string message = result > 0 ? "Deleting successful." : "Deleting failed.";
        return new ResponseModel()
        {
            IsSuccessful = result > 0,
            Message = message
        };
    }
}
