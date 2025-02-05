using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    [Table("TBL_Traveler")]
    public class TravelerModel
    {
        [Key]
        public string Id { get; set; } = null!;

        public string BookingId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public int Age { get; set; }

        public string Gender { get; set; } = null!;
    }
}
