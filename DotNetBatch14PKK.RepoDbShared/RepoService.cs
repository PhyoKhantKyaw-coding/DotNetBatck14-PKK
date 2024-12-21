using Dapper;
using Microsoft.Data.SqlClient;
using RepoDb;
using System.Data;

namespace DotNetBatch14PKK.RepoDbShared;

public class RepoService
{
    private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;
    public readonly IDbConnection _dbConnection;
    public RepoService()
    {
        _dbConnection = new SqlConnection(_connectionString);
        RepoDb.SqlServerBootstrap.Initialize();
    }
    public List<BlogModel> GetBlogs()
    {
        var blogs = _dbConnection.QueryAll<BlogModel>().ToList();
        return blogs;
    }
    public BlogModel GetBlog(string id)
    {
        var blog = _dbConnection.Query<BlogModel>(id).FirstOrDefault();
        return blog!;
    }
    public ResponseModel Create(string title, string author, string content)
    {
        var blog = new BlogModel
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        };

        var result = _dbConnection.Insert<BlogModel,int>(blog);
        string message = result > 0 ? "Saving successful." : "Saving failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
    public ResponseModel Update(BlogModel requestModel)
    {
        var blog = GetBlog(requestModel.BlogId!);
        if (blog is null) return new()
        {
            IsSuccessful = false,
            Message = "No data found."
        };

        if (!String.IsNullOrEmpty(requestModel.BlogTitle))
        {
            blog.BlogTitle = requestModel.BlogTitle;
        }
        else if (!String.IsNullOrEmpty(requestModel.BlogAuthor))
        {
            blog.BlogAuthor = requestModel.BlogAuthor;
        }
        else if (!String.IsNullOrEmpty(requestModel.BlogContent))
        {
            blog.BlogContent = requestModel.BlogContent;
        }
        var result = _dbConnection.Update(blog);
        string message = result > 0 ? "Updating successful." : "Updating failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
    public ResponseModel DeleteBlog(string id)
    {
        var result = _dbConnection.Delete<BlogModel>(id);
        string message = result > 0 ? "Deleting successful." : "Deleting failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
}
