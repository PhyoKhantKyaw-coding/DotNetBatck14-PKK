using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14PKK.RestaurantConsole.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderCode { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
 