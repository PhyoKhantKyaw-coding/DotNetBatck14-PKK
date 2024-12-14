using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DotNetBatch14PKK.ConsoleApp6
{
    public class GameHttpsClient
    {
        private readonly string baseEndpoint = "https://localhost:7105/api/snake";
        private readonly HttpClient _client;

        public GameHttpsClient()
        {
            _client = new HttpClient();
        }

        public async Task<ResponseModel> CreateGameBoard()
        {
            HttpResponseMessage response = await _client.GetAsync($"{baseEndpoint}/Create%20GameBoard");
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }


        public async Task<ResponseModel> CreateGame(List<PlayerModel> players)
        {
            string jsonStr = JsonConvert.SerializeObject(players);
            var content = new StringContent(jsonStr, Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _client.PostAsync($"{baseEndpoint}/CreateGame", content);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }


        public async Task<ResponseModel> PlayGame(int playerId)
        {
            var content = new StringContent(JsonConvert.SerializeObject(playerId), Encoding.UTF8, Application.Json);
            HttpResponseMessage response = await _client.PostAsync($"{baseEndpoint}/PlayGame?firstPlayerId={playerId}", content);
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }
    }
}