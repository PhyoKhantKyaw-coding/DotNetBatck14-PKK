using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.snakeConsoleApp
{
    internal class SnakeHttpsClientService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseEndpoint = "https://burma-project-ideas.vercel.app";

        public SnakeHttpsClientService()
        {
            _httpClient = new HttpClient();
        }
        public async Task<List<Rootobject>> GetSnakes()
        {
            var response = await _httpClient.GetAsync($"{_baseEndpoint}/snakes");
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Rootobject>>(json)!;
        }
        public async Task<Rootobject> GetSnake(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseEndpoint}/snake/{id}");
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Rootobject>(json)!;
        }
    }
}
