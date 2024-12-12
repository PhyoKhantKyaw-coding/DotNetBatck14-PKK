using Microsoft.AspNetCore.Mvc;
using DotNetBatch14PKK.SnakesGame.Features.SnakesGame;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly EfcorePlayersServices _playerServices;
        private readonly EFcoreGameBoardServices _gameBoardServices;
        private readonly EfcoreGameMoveServices _gameMoveServices;
        private readonly EfcoreGameServices _gameServices;

        public GameController()
        {
            _playerServices = new EfcorePlayersServices();
            _gameBoardServices = new EFcoreGameBoardServices();
            _gameMoveServices = new EfcoreGameMoveServices(new AppDbContent());
            _gameServices = new EfcoreGameServices();
        }

        [HttpGet("Players")]
        public IActionResult GetPlayers()
        {
            var players = _playerServices.GetPlayers();
            return Ok(players);
        }

        [HttpGet("Player/{playerId}")]
        public IActionResult GetPlayerById(int playerId)
        {
            var player = _playerServices.GetPlayerById(playerId);
            if (player == null) return NotFound("Player not found.");
            return Ok(player);
        }

        [HttpPost("RegisterPlayer")]
        public IActionResult CreatePlayer([FromBody] PlayerModel requestModel)
        {
            var model = _playerServices.CreatePlayer(requestModel);
            if (!model.IsSuccessful) return BadRequest(model);
            return Ok(model);
        }

        [HttpPost("UpdatePosition")]
        public IActionResult UpdatePlayerPosition(int playerId, int currentPosition)
        {
            var model = _playerServices.UpdatePlayerPosition(playerId, currentPosition);
            if (!model.IsSuccessful) return BadRequest(model);
            return Ok(model);
        }

        [HttpGet("GameBoards")]
        public IActionResult GetAllGameBoards()
        {
            var boards = _gameBoardServices.GetAllGameBoard();
            return Ok(boards);
        }

        [HttpGet("GameBoard/{boardId}")]
        public IActionResult GetGameBoardById(int boardId)
        {
            var board = _gameBoardServices.GetGameBoardById(boardId);
            if (board == null) return NotFound("Game board not found.");
            return Ok(board);
        }

        [HttpPost("CreateGameBoard")]
        public IActionResult CreateGameBoard([FromBody] GameBoardModel gameBoard)
        {
            var response = _gameBoardServices.CreateGameBoard(gameBoard);
            if (!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("UpdateGameBoard")]
        public IActionResult UpdateGameBoard([FromBody] GameBoardModel gameBoard)
        {
            var response = _gameBoardServices.UpdateGameBoard(gameBoard);
            if (!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }

        [HttpDelete("DeleteGameBoard/{boardId}")]
        public IActionResult DeleteGameBoard(int boardId)
        {
            var response = _gameBoardServices.DeleteGameBoard(boardId);
            if (!response.IsSuccessful) return NotFound(response);
            return Ok(response);
        }

        [HttpGet("GameMoves")]
        public IActionResult GetAllGameMoves()
        {
            var moves = _gameMoveServices.GetAllGameMoves();
            return Ok(moves);
        }

        [HttpGet("GameMoves/{gameId}")]
        public IActionResult GetGameMovesByGameId(int gameId)
        {
            var moves = _gameMoveServices.GetGameMovesByGameId(gameId);
            return Ok(moves);
        }

        [HttpPost("PlayGame")]
        public IActionResult PlayGame(int firstPlayerId, int secondPlayerId)
        {
            var response = _gameMoveServices.PlayGame(firstPlayerId, secondPlayerId);
            return Ok(response);
        }

        [HttpGet("Games")]
        public IActionResult GetGames()
        {
            var games = _gameServices.GetGames();
            return Ok(games);
        }

        [HttpGet("Game/{gameId}")]
        public IActionResult GetGameById(int gameId)
        {
            var game = _gameServices.GetGameById(gameId);
            if (game == null) return NotFound("Game not found.");
            return Ok(game);
        }

        [HttpPost("CreateGame")]
        public IActionResult CreateGame([FromBody] GameModel game)
        {
            var response = _gameServices.CreateGame(game);
            if (!response.IsSuccessful) return BadRequest(response);
            return Ok(response);
        }

        [HttpPut("UpdateCurrentPlayer")]
        public IActionResult UpdateCurrentPlayer(int gameId, int currentPlayerId)
        {
            var response = _gameServices.UpdateCurrentPlayerId(gameId, currentPlayerId);
            if (!response.IsSuccessful) return NotFound(response);
            return Ok(response);
        }
    }
}
