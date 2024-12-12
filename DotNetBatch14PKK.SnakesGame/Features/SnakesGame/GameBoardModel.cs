using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.SnakesGame.Features.SnakesGame
{
    [Table("tblGameBoard")]
    public class GameBoardModel
    {
        [Key]
        public int BoardID { get; set; }
        public int CellNumber { get; set; }
        public string? CellType { get; set; }
        public int? MoveToCell { get; set; }
    }
}
