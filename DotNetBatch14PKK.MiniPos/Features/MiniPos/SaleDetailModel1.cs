using DotNetBatch14PKK.MiniPos.Features.MiniPos;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNet_Batch14PKK.MiniPos.Features
{
    [Table("tblSaledetail")]
    public class SaleDetailModel
    {
        [Key]
        public string? SaleDetailId { get; set; }
        [Required]
        public int ProductCode { get; set; }
        [Required]
        public int Qty { get; set; } 
        public decimal TotalPrice { get; set; } 
        [Required]
        public int VoucherNo { get; set; }

    }
}