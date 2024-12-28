using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.RestaurantConsole.Models
{
    public class OrderItem
    {
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
}
