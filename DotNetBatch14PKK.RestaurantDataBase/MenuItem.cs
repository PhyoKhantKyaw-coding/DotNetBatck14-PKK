using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.RestaurantDataBase;
[Table("MenuItem")]
public class MenuItem
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int MenuItemCode { get; set; }
    public decimal Price { get; set; }
}
 