using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame;

public class EfcoreGameMoveServices
{
    private readonly AppDbContent _db;
    private readonly Random _random;
    private readonly EfcorePlayersServices _playerServices;
    private readonly EfcoreGameServices _gameServices;
    public EfcoreGameMoveServices(AppDbContent db)
    {
        _gameServices = new EfcoreGameServices();
        _playerServices = new EfcorePlayersServices();
        _db = db;
        _random = new Random();
    }

    public List<GameMoveModel> GetAllGameMoves()
    {
        return _db.gameMove
            .Include(gm => gm.Game)
            .Include(gm => gm.Player)
            .AsNoTracking()
            .ToList();
    }

    public List<GameMoveModel> GetGameMovesByGameId(int gameId)
    {
        return _db.gameMove
            .Include(gm => gm.Game)
            .Include(gm => gm.Player)
            .AsNoTracking()
            .Where(gm => gm.GameID == gameId)
            .ToList();
    }

    public PlayerCurrentResponseModel PlayGame(int firstPlayerId, int secondPlayerId)
    {
        var game = _db.game.AsNoTracking().FirstOrDefault(g => g.CurrentPlayerID == firstPlayerId);

        if (game == null)
        {
            return new PlayerCurrentResponseModel
            {
                Message = "Game not found."
            };
        }

        var firstPlayer = _db.player.AsNoTracking().FirstOrDefault(p => p.PlayerID == firstPlayerId);
        var secondPlayer = _db.player.AsNoTracking().FirstOrDefault(p => p.PlayerID == secondPlayerId);

        if (firstPlayer == null || secondPlayer == null)
        {
            return new PlayerCurrentResponseModel
            {
                Message = "Player(s) not found."
            };
        }

        if (game.CurrentPlayerID != firstPlayerId)
        {
            return new PlayerCurrentResponseModel
            {
                Message = "It is not the first player's turn."
            };
        }

        game.CurrentPlayerID = secondPlayer.PlayerID;
        var gameUpdateResponse = _gameServices.UpdateCurrentPlayerId(game.GameID, secondPlayer.PlayerID);

        if (!gameUpdateResponse.IsSuccessful)
        {
            return new PlayerCurrentResponseModel
            {
                Message = "Failed to update the current player."
            };
        }

        int dice1 = _random.Next(1, 7);
        int dice2 = _random.Next(1, 7);
        int totalMove = dice1 + dice2;

        int fromCell = firstPlayer.CurrentPosition;
        int newPosition = fromCell + totalMove;

        var currentCell = _db.gameBoard.FirstOrDefault(cb => cb.CellNumber == newPosition);

        var gameMove = new GameMoveModel
        {
            GameID = game.GameID,
            PlayerID = firstPlayer.PlayerID,
            FromCell = fromCell,
            ToCell = newPosition,
            MoveDate = DateTime.Now
        };

        string message;

        if (currentCell == null || currentCell.CellType == "Normal")
        {
            firstPlayer.CurrentPosition = newPosition;
            _playerServices.UpdatePlayerPosition(firstPlayerId, newPosition);
            message = $"It is {secondPlayer.PlayerName}'s turn.";
        }
        else if (currentCell.CellType == "SnakeHead")
        {
            newPosition = currentCell.MoveToCell ?? 0;
            _playerServices.UpdatePlayerPosition(firstPlayerId, newPosition);
            gameMove.ToCell = newPosition;
            message = $"Oops! {firstPlayer.PlayerName} landed on a snake head and moved to {newPosition}.";
        }
        else if (currentCell.CellType == "LadderBottom")
        {
            newPosition = currentCell.MoveToCell ?? 0;
            _playerServices.UpdatePlayerPosition(firstPlayerId, newPosition);
            gameMove.ToCell = newPosition;
            message = $"Hurray! {firstPlayer.PlayerName} climbed a ladder to {newPosition}.";
        }
        else
        {
            message = $"It is {secondPlayer.PlayerName}'s turn.";
        }

        _db.gameMove.Add(gameMove);
        _db.SaveChanges();

        if (newPosition == 100)
        {
            message = $"{firstPlayer.PlayerName} wins the game!";
            game.GameStatus = "Completed";
            _db.Entry(game).State = EntityState.Modified;
            _db.SaveChanges();
        }

        return new PlayerCurrentResponseModel
        {
            firstPosition = fromCell.ToString(),
            lastPosition = newPosition.ToString(),
            Message = message
        };
    }
}
