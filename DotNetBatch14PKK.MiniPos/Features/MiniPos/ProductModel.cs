using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.MiniPos.Features.MiniPos
{
    [Table("tblProduct")]
    public class ProductModel
    {
        [Key]
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public string? CategoryId { get; set; }
    }
}
