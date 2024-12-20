using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.BurmaProjectSnake
{
    internal class SnakeRestSharpClientService
    {
        private readonly string endpoint = "https://burma-project-ideas.vercel.app";
        private readonly RestClient _restClient;
        public SnakeRestSharpClientService()
        {
            _restClient = new RestClient(endpoint);
        }
        public async Task<List<Rootobject>> GetSnakes()
        {
            RestRequest request = new RestRequest("snakes", Method.Get);
            var response = await _restClient.GetAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<List<Rootobject>>(json)!;
        }
        public async Task<Rootobject> GetSnake(int id)
        {
            RestRequest request = new RestRequest($"snake/{id}", Method.Get);
            var response = await _restClient.GetAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<Rootobject>(json)!;
        }
    }
}
