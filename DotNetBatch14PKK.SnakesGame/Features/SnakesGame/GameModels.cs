using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame
{
    [Table("tblGames")]
    public class GameModel
    {
        [Key]
        public int GameID { get; set; }
        public string? GameStatus { get; set; }
        public int? CurrentPlayerID { get; set; }
        [ForeignKey("CurrentPlayerID")]
        public PlayerModel? CurrentPlayer { get; set; }
    }
}
