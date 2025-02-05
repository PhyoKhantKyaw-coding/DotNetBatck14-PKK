using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    [Table("TBL_Payments")]
    public class PaymentModel
    {
        [Key]
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string BookingId { get; set; } = null!;

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentStatus { get; set; } = null!;
    }
}
