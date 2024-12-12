using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame
{
    [Table("tblGameMoves")]
    public class GameMoveModel
    {
        [Key]
        public int MoveID { get; set; }
        public int GameID { get; set; }
        [ForeignKey("GameID")]
        public GameModel? Game { get; set; }
        public int PlayerID { get; set; }
        [ForeignKey("PlayerID")]
        public PlayerModel? Player { get; set; }
        public int FromCell { get; set; }
        public int ToCell { get; set; }
        public DateTime MoveDate { get; set; } = DateTime.Now;
    }
}
