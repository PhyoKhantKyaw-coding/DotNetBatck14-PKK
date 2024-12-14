using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp6
{
    public class GameRestSharpClient
    {
        private readonly string endpoint = "https://localhost:7105/api/snake";
        private readonly RestClient _restClient;

        public GameRestSharpClient()
        {
            _restClient = new RestClient();
        }

        public async Task<ResponseModel> CreateGameBoard()
        {
            RestRequest request = new RestRequest($"{endpoint}/CreateGameBoard", Method.Get);
            var response = await _restClient.GetAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }

        public async Task<ResponseModel> CreateGame(List<PlayerModel> players)
        {
            RestRequest request = new RestRequest($"{endpoint}/CreateGame", Method.Post);
            request.AddJsonBody(players);
            var response = await _restClient.PostAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }


        public async Task<ResponseModel> PlayGame(int playerId)
        {
            RestRequest request = new RestRequest($"{endpoint}/{playerId}", Method.Post);
            var response = await _restClient.PostAsync(request);
            string json = response.Content!;
            return JsonConvert.DeserializeObject<ResponseModel>(json)!;
        }

    }

}
