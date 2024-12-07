using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PKK.MiniPos.Features.MiniPos
{
    [Table("tblProduct")]
    public class ProductModel
    {
        [Key]
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public int ProductCode { get; set; }
        public int CatCode { get; set; }
    }
}
