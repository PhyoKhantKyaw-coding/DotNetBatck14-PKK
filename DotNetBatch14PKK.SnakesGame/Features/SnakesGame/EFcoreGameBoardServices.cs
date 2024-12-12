using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame;

public class EFcoreGameBoardServices
{
    private readonly AppDbContent _db;

    public EFcoreGameBoardServices()
    {
        _db = new AppDbContent();
    }

    public List<GameBoardModel> GetAllGameBoard()
    {
        return _db.gameBoard.AsNoTracking().ToList();
    }

    public GameBoardModel? GetGameBoardById(int boardId)
    {
        return _db.gameBoard.AsNoTracking().FirstOrDefault(board => board.BoardID == boardId);
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

    public ResponseModel UpdateGameBoard(GameBoardModel gameBoard)
    {
        var existingBoard = _db.gameBoard.FirstOrDefault(board => board.BoardID == gameBoard.BoardID);
        if (existingBoard == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Game board not found."
            };
        }

        existingBoard.CellNumber = gameBoard.CellNumber;
        existingBoard.CellType = gameBoard.CellType;
        existingBoard.MoveToCell = gameBoard.MoveToCell;

        _db.Entry(existingBoard).State = EntityState.Modified;
        var result = _db.SaveChanges();
        string message = result > 0 ? "Game board updated successfully." : "Failed to update game board.";
        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = message
        };
    }

    public ResponseModel DeleteGameBoard(int boardId)
    {
        var board = _db.gameBoard.FirstOrDefault(b => b.BoardID == boardId);
        if (board == null)
        {
            return new ResponseModel
            {
                IsSuccessful = false,
                Message = "Game board not found."
            };
        }

        _db.gameBoard.Remove(board);
        var result = _db.SaveChanges();
        string message = result > 0 ? "Game board deleted successfully." : "Failed to delete game board.";
        return new ResponseModel
        {
            IsSuccessful = result > 0,
            Message = message
        };
    }
}

