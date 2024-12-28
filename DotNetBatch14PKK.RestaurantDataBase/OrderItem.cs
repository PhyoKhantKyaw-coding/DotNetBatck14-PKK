using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetBatch14PKK.RestaurantDataBase;
[Table("OrderItem")]
public class OrderItem
{
    [Key]
    public int Id { get; set; }
    public int OrderCode { get; set; }
    public int MenuItemCode { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }

} 
public class Itemlist
{
    public int MenuItemCode { get; set; }
    public int Quantity { get; set; }
}
