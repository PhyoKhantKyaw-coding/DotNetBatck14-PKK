using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.SnakesGame2.Features.SnakesGame2;

[Table("TBL_Players")]
public class PlayerModel
{
    [Key]
    public int PlayerID { get; set; }
    public string? PlayerName { get; set; }
    public int CurrentPosition { get; set; }
}
