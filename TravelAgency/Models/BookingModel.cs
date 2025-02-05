using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    [Table("TBL_Booking")]
    public class BookingModel
    {
        [Key]
        public string Id { get; set; } = null!;

        public string UserId { get; set; } = null!;

        public string TravelPackageId { get; set; } = null!;

        public int NumberOfTravelers { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime BookingDate { get; set; }

        public string Status { get; set; } = null!;
    }
}
