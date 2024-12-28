using Refit;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp5
{
    internal class BlogRefitClientServices
    {
        private readonly IBlogApi _blogApi;

        public BlogRefitClientServices()
        {
            _blogApi = RestService.For<IBlogApi>("https://localhost:7264");
        }

        public async Task<BlogListResponseModel> GetBlogs()
        {
            return await _blogApi.GetBlogs();
        }

        public async Task<ResponseModel> GetBlog(string id)
        {
            return await _blogApi.GetBlog(id);
        }

        public async Task<ResponseModel> CreateBlog(BlogModel requestModel)
        {
            return await _blogApi.CreateBlog(requestModel);
        }

        public async Task<ResponseModel> UpdateBlog(string id, BlogModel requestModel)
        {
            return await _blogApi.UpdateBlog(id, requestModel);
        }

        public async Task<ResponseModel> DeleteBlog(string id)
        {
            return await _blogApi.DeleteBlog(id);
        }
    }
    public interface IBlogApi
    {
        [Get("/api/Blog")]
        Task<BlogListResponseModel> GetBlogs();

        [Get("/api/Blog/{id}")]
        Task<ResponseModel> GetBlog(string id);

        [Post("/api/Blog")]
        Task<ResponseModel> CreateBlog([Body] BlogModel requestModel);

        [Patch("/api/Blog/{id}")]
        Task<ResponseModel> UpdateBlog(string id, [Body] BlogModel requestModel);

        [Delete("/api/Blog/{id}")]
        Task<ResponseModel> DeleteBlog(string id);
    }
}
