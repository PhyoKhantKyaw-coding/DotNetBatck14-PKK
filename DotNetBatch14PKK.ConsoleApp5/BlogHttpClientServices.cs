using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetBatch14PKK.ConsoleApp5
{
    public class BlogHttpClientServices
    {
        private readonly string endpoint = "https://localhost:7264/api/Blog";
        private readonly HttpClient _client;
        public BlogHttpClientServices()
        {
            _client = new HttpClient();
        }
        public async Task<BlogListResponseModel> GetBlogs()
        {            
            HttpResponseMessage response = await _client.GetAsync(endpoint);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BlogListResponseModel>(json)!;
   
        }
        public async Task<ResponseModel> GetBlog(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{ endpoint}/{id}");
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;

        }
        public async Task<ResponseModel> createBlog(BlogModel requestModel)
        {
            string jsonstr = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonstr,Encoding.UTF8,Application.Json);
            HttpResponseMessage response = await _client.PostAsync(endpoint,content);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;

        }
        public async Task<ResponseModel> UpdateBlog( BlogModel requestModel)
        {
            string jsonstr = JsonConvert.SerializeObject(requestModel);
            var content = new StringContent(jsonstr, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _client.PatchAsync($"{endpoint}/{requestModel.BlogId}", content);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }

        public async Task<ResponseModel> DeleteBlog(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{endpoint}/{id}");
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }
    }
}
