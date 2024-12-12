using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame;

public class EfcorePlayersServices
{
    private readonly AppDbContent _db;

    public EfcorePlayersServices()
    {
        _db = new AppDbContent();
    }

    public List<PlayerModel> GetPlayers()
    {
        return _db.player.AsNoTracking().ToList();
    }

    public PlayerModel? GetPlayerById(int playerId)
    {
        return _db.player.AsNoTracking().FirstOrDefault(player => player.PlayerID == playerId);
    }

    public ResponseModel CreatePlayer(PlayerModel player)
    {
        _db.player.Add(player);
        var result = _db.SaveChanges();
        string message = result > 0 ? "Player created successfully." : "Player creation failed.";
        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = message
        };
    }

    public ResponseModel UpdatePlayerPosition(int playerId, int currentPosition)
    {
        var player = _db.player.FirstOrDefault(p => p.PlayerID == playerId);
        if (player == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Player not found."
            };
        }

        player.CurrentPosition = currentPosition;
        _db.Entry(player).State = EntityState.Modified;
        var result = _db.SaveChanges();
        string message = result > 0 ? "Player position updated successfully." : "Failed to update player position.";
        return new ResponseModel
        {
            IsSuccessful = true,
            Message = message
        };
    }
}