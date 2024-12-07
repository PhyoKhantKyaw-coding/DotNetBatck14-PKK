using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DotNetBatch14PKK.Mini_Pos.Features.MiniPos
{
    [Table("tblCategory")]
    public class CategoryModel
    {
        [Key]
        public string? CategoryId { get; set; }
        public string? CatName { get; set; }
    }

    public class ResponseModel
    {
        public bool IsSuccessful { get; set; }
        public string? Message { get; set; }
    }
}
