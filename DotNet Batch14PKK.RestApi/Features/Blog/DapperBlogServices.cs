using Dapper;
using DotNetBatch14PKK.RestApi;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNet_Batch14PKK.RestApi.Features.Blog
{
    public class BlogDapperService
    {
        private readonly string _connectionString = AppSettings.connectionBuilder.ConnectionString;
        public List<BlogModel> GetBlogs()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            string query = "select * from TBL_Blog with (nolock)";
            var blogs = connection.Query<BlogModel>(query).ToList();
            return blogs;
        }

        public BlogModel GetBlog(string id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);
            string query = "select * from TBL_Blog with (nolock) where BlogId = @BlogId";
            var blog = connection.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });
            return blog!;
        }

        public ResponseModel CreateBlog(BlogModel requestModel)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent);";
            var result = connection.Execute(query, requestModel);

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

            using IDbConnection connection = new SqlConnection(_connectionString);

            string query = $@"UPDATE [dbo].[TBL_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
            var result = connection.Execute(query, requestModel);

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
                if (requestModel.BlogTitle is null || requestModel.BlogAuthor is null || requestModel.BlogContent is null)
                {
                    responseModel.IsSuccessful = false;
                    responseModel.Message = "Updating failed.";

                    return responseModel;
                }

                using IDbConnection connection = new SqlConnection(_connectionString);

                string query = $@"UPDATE [dbo].[TBL_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";
                var result = connection.Execute(query, requestModel);

                string message = result > 0 ? "Updating successful." : "Updating failed.";

                responseModel.IsSuccessful = result > 0;
                responseModel.Message = message;

                return responseModel;
            }
            else if (blog is null)
            {
                responseModel = CreateBlog(requestModel);
            }

            return responseModel;
        }

        public ResponseModel DeleteBlog(string id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            string query = "delete from TBL_Blog where BlogId = @BlogId";
            var result = connection.Execute(query, new BlogModel { BlogId = id });

            string message = result > 0 ? "Deleting successful." : "Deleting failed.";

            return new ResponseModel()
            {
                IsSuccessful = result > 0,
                Message = message
            };
        }
    }

    
}
