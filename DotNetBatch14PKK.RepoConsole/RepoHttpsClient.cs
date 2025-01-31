﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetBatch14PKK.RepoConsole
{
    internal class RepoHttpsClient
    {
        private readonly string Endpoint = "https://localhost:7232/api/blog";
        private readonly HttpClient _client;

        public RepoHttpsClient()
        {
            _client = new HttpClient();
        }

        public async Task<BlogListResponseModel> GetBlogs()
        {
            HttpResponseMessage response = await _client.GetAsync($"{Endpoint}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BlogListResponseModel>(json)!;
        }

        public async Task<BlogModel> GetBlog(string id)
        {
            HttpResponseMessage response = await _client.GetAsync($"{Endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BlogModel>(json)!;
        }

        public async Task<ResponseModel> CreateBlog(BlogModel blog)
        {
            string jsonStr = JsonConvert.SerializeObject(blog);
            var content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _client.PostAsync($"{Endpoint}", content);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }

        public async Task<ResponseModel> UpdateBlog(string id, BlogModel blog)
        {
            blog.BlogId = id;
            string jsonStr = JsonConvert.SerializeObject(blog);
            var content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _client.PatchAsync($"{Endpoint}/{id}", content);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }

        public async Task<ResponseModel> DeleteBlog(string id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"{Endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }
    }


}
