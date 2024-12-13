using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame;

public class EfcoreGameServices
{
    private readonly AppDbContent _db;

    public EfcoreGameServices()
    {
        _db = new AppDbContent();
    }

    public List<GameModel> GetGames()
    {
        return _db.game
            .AsNoTracking()
            .ToList();
    }

    public GameModel? GetGameById(int gameId)
    {
        return _db.game
            .AsNoTracking()
            .FirstOrDefault(game => game.GameID == gameId);
    }

    public ResponseModel CreateGame(GameModel game)
    {
        _db.game.Add(game);
        var result = _db.SaveChanges();
        string message = result > 0 ? "Game created successfully." : "Game creation failed.";
        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = message
        };
    }

    public ResponseModel UpdateCurrentPlayerId(int gameId, int currentPlayerId)
    {
        var game = _db.game.FirstOrDefault(g => g.GameID == gameId);
        if (game == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Game not found."
            };
        }

        game.CurrentPlayerID = currentPlayerId;
        _db.Entry(game).State = EntityState.Modified;
        var result = _db.SaveChanges();
        string message = result > 0 ? "Current player updated successfully." : "Failed to update current player.";
        return new ResponseModel
        {
            IsSuccessful = true,
            Message = message
        };
    }
}
