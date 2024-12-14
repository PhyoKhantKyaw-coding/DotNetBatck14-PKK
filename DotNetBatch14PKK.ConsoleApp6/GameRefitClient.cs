using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.ConsoleApp6
{
    public class GameRefitClient
    {
        private readonly IGameApi _gameApi;

        public GameRefitClient()
        {            
            _gameApi = RestService.For<IGameApi>("https://localhost:7105");
        }

        public async Task<ResponseModel> CreateGameBoard()
        {
            return await _gameApi.CreateGameBoard();
        }

        public async Task<ResponseModel> CreateGame(List<PlayerModel> players)
        {
            return await _gameApi.CreateGame(players);
        }

        public async Task<ResponseModel> PlayGame(int playerId)
        {
            return await _gameApi.PlayGame(playerId);
        }
    }
    public interface IGameApi
    {
        [Get("/api/snake/CreateGameBoard")]
        Task<ResponseModel> CreateGameBoard();

        [Post("/api/snake/CreateGame")]
        Task<ResponseModel> CreateGame([Body] List<PlayerModel> players);

        [Post("/api/snake/{playerId}")]
        Task<ResponseModel> PlayGame(int playerId);
    }
}
