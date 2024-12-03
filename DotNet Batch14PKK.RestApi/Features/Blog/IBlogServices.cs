
namespace DotNet_Batch14PKK.RestApi.Features.Blog
{
    public interface IBlogServices
    {
        ResponseModel CreateBlog(BlogModel requestModel);
        ResponseModel DeleteBlog(string id);
        BlogModel GetBlog(string id);
        List<BlogModel> GetBlogs();
        ResponseModel UpdateBlog(BlogModel requestModel);
        ResponseModel UpsertBlog(BlogModel requestModel);
    }
}