using Newtonsoft.Json;
using RestSharp;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetBatch14PKK.ConsoleApp5
{
    public class BlogRestClientServices
    {
        private readonly string endpoint = "https://localhost:7264/api/Blog";
        private readonly RestClient _restClient;
        public BlogRestClientServices()
        {
            _restClient = new RestClient();
        }
        public async Task<BlogListResponseModel> GetBlogs()
        {
            RestRequest request = new RestRequest(endpoint, Method.Get);
            var response = await _restClient.GetAsync(request);
            string json =  response.Content!;
            return JsonConvert.DeserializeObject<BlogListResponseModel>(json)!;
   
        }
        public async Task<ResponseModel> GetBlog(string id)
        {
            RestRequest request= new RestRequest($"{endpoint}/{id}", Method.Get);
            var response = await _restClient.GetAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;

        }
        public async Task<ResponseModel> createBlog(BlogModel requestModel)
        {
            RestRequest request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(requestModel);
            var response = await _restClient.PostAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;

        }
        public async Task<ResponseModel> UpdateBlog( BlogModel requestModel)
        {
            RestRequest request =new RestRequest($"{endpoint}/{requestModel.BlogId}", Method.Patch);
            request.AddJsonBody(requestModel);
            var response = await _restClient.PatchAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }

        public async Task<ResponseModel> DeleteBlog(string id)
        {
            RestRequest request = new RestRequest($"{endpoint}/{id}", Method.Delete);
            var response = await _restClient.DeleteAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;

        }
    }
}
