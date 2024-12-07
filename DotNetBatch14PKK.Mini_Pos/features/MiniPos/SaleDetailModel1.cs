using DotNetBatch14PKK.Mini_Pos.Features.MiniPos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.Mini_Pos.Features.MiniPos
{
    [Table("tblSaledetail")]
    public class SaleDetailModel
    {
        [Key]
        public string? SaleDetailId { get; set; }
        [Required]
        public string? ProductId { get; set; }
        [Required]
        public int Qty { get; set; } 
        public int TotalPrice { get; set; } 
        [Required]
        public string? SaleId { get; set; }

    }
}