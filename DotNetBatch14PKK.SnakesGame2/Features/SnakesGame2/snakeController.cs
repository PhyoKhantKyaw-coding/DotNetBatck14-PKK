using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PKK.SnakesGame2.Features.SnakesGame2
{
    [Route("api/[controller]")]
    [ApiController]
    public class snakeController : ControllerBase
    {
        private readonly EfcoreGameServices _gameServices;

        public snakeController()
        {
            _gameServices = new EfcoreGameServices();
        }

        [HttpGet("Create GameBoard")]
        public IActionResult CreateGameBoard( GameBoardModel gameBoard)
        {
            try
            {
                var response = _gameServices.CreateGameBoard(gameBoard);
                if (!response.IsSuccessful) return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }

        [HttpPost("CreateGame")]
        public IActionResult CreateGame(List<PlayerModel> playerlist)
        {
            try
            {
                var response = _gameServices.CreateGame(playerlist);
                if (!response.IsSuccessful) return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }

        [HttpPost("PlayGame")]
        public IActionResult PlayGame(int firstPlayerId)
        {
            try
            {
                var response = _gameServices.PlayGame(firstPlayerId);
                if (!response.IsSuccessful) return BadRequest(response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseModel
                {
                    Message = ex.ToString(),
                });
            }
        }
    }
}
