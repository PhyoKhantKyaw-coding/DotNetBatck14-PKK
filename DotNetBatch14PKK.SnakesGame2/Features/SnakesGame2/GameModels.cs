using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.SnakesGame2.Features.SnakesGame2
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
        public int? members {  get; set; }  
    }
}
