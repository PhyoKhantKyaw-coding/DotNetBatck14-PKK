using Microsoft.EntityFrameworkCore;


namespace DotNetBatch14PKK.SnakesGame2.Features.SnakesGame2;

public class EfcoreGameServices
{
    private readonly AppDbContent _db;

    private readonly Random _random;

    public EfcoreGameServices()
    {
        _db = new AppDbContent();
        _random = new Random();
    }

    public List<GameModel> GetGames()
    {
        return _db.game
            .AsNoTracking()
            .ToList();
    }
    public ResponseModel CreateGameBoard(GameBoardModel gameBoard)
    {
        _db.gameBoard.Add(gameBoard);
        var result = _db.SaveChanges();
        string message = result > 0 ? "Game board created successfully." : "Failed to create game board.";
        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = message
        };
    }
    public ResponseModel CreateGame(List<PlayerModel> playersArray)
    {
        int firstPlayerID = 0;

        for (int i = 0; i < playersArray.Count; i++)
        {
            var player = playersArray[i];
            _db.player.Add(player);
            _db.SaveChanges();

            if (i == 0)
            {
                firstPlayerID = player.PlayerID;
            }
        }

        var game = new GameModel
        {
            GameStatus = "InProgress",
            CurrentPlayerID = firstPlayerID,
            members = playersArray.Count
        };
        _db.game.Add(game);
        _db.SaveChanges();

        foreach (var player in playersArray)
        {
            var initialGameMove = new GameMoveModel
            {
                GameID = game.GameID,
                PlayerID = player.PlayerID,
                FromCell = 0,
                ToCell = 0,
                MoveDate = DateTime.Now
            };
            _db.gameMove.Add(initialGameMove);
        }

        var result = _db.SaveChanges();

        string message = result > 0 ? "Game created successfully." : "Game creation failed.";
        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = message
        };
    }


    public ResponseModel PlayGame(int id)
    {
        var game = _db.game.AsNoTracking().FirstOrDefault(g => g.CurrentPlayerID == id);
        var player = _db.player.AsNoTracking().FirstOrDefault(p => p.PlayerID == id);

        if (game == null)
        {
            return new ResponseModel
            {
                Message = "It is not your turn!"
            };
        }

        int dice1 = _random.Next(1, 7);
        int dice2 = _random.Next(1, 7);
        int totalMove = dice1 + dice2;

        int fromcell = player.CurrentPosition;
        int newPosition = fromcell + totalMove;

        var gameMove = new GameMoveModel
        {
            GameID = game.GameID,
            PlayerID = player.PlayerID,
            FromCell = fromcell,
            MoveDate = DateTime.Now
        };

        string message = $"{player.PlayerName} moved from {fromcell} to {newPosition}.";
        var gameboard = _db.gameBoard.ToList();
        if (newPosition >= 100)
        {
            message = $"{player.PlayerName} wins the game!";
            game.GameStatus = "Completed";
            _db.Entry(game).State = EntityState.Modified;
            _db.SaveChanges();
        }
        var allPlayers = _db.gameMove
            .Where(gm => gm.GameID == game.GameID)
            .Select(gm => gm.PlayerID)
            .Distinct()
            .OrderBy(id => id)
            .ToList();

        var currentIndex = allPlayers.FindIndex(p => p == player.PlayerID);
        int nextIndex = (currentIndex < allPlayers.Count - 1) ? currentIndex + 1 : 0;

        game.CurrentPlayerID = allPlayers[nextIndex];
        _db.Entry(game).State = EntityState.Modified;
        _db.SaveChanges();
        var nextplayer  = _db.player.AsNoTracking().FirstOrDefault(p=>p.PlayerID==game.CurrentPlayerID);
        foreach (var cell in gameboard)
        {
            if (cell.CellNumber == newPosition)
            {
                if (cell.CellType == "Normal")
                {
                    gameMove.ToCell = newPosition;
                    player.CurrentPosition = newPosition;
                    message = $"{player.PlayerName} moved from {fromcell} to {newPosition} and {nextplayer.PlayerName} turns.";
                }
                else if (cell.CellType == "SnakeHead")
                {
                    newPosition = cell.MoveToCell ?? 0;
                    gameMove.ToCell = newPosition;
                    player.CurrentPosition = newPosition;
                    message = $"{player.PlayerName} landed on a snake head and moved from {fromcell + totalMove} to {newPosition} and {nextplayer.PlayerName} turns.";
                }
                else if (cell.CellType == "LadderBottom")
                {
                    newPosition = cell.MoveToCell ?? 0;
                    gameMove.ToCell = newPosition;
                    player.CurrentPosition = newPosition;
                    message = $"{player.PlayerName} climbed a ladder from {fromcell + totalMove} to {newPosition} and {nextplayer.PlayerName} turns.";
                }
                break;
            }
        }
        player.CurrentPosition = newPosition;
        _db.Entry(player).State = EntityState.Modified;
        _db.SaveChanges();



        _db.gameMove.Add(gameMove);
        _db.SaveChanges();

        return new ResponseModel
        {
            IsSuccessful = true,
            Message = message
        };
    }
}
