using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.RestaurantDataBase;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }
    public int OrderCode { get; set; }
    public decimal TotalPrice { get; set; }
}
 