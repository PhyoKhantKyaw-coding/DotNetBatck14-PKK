using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.MiniPos.Features.MiniPos
{
    [Table("tblSale")]
    public class SaleModel
    {
        [Key]
        public string? SaleId { get; set; }
        public DateTime SaleDate { get; set; }
        public int STotalQty { get; set; } 
        public int STA { get; set; } 
    }
}
