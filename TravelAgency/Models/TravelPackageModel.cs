using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Models
{
    [Table("TBL_TravelPackage")]
    public class TravelPackageModel
    {
        [Key]

        public string Title { get; set; } = null!;

        public string Destination { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string? Inclusions { get; set; }

        public string? CancellationPolicy { get; set; }

        public string Status { get; set; } = null!;

        // New property for storing photo file path
        public string? PhotoPath { get; set; }
    }
    public class TravelPackageRequestModel
    {
        public string Title { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Inclusions { get; set; }
        public string? CancellationPolicy { get; set; }
        public string Status { get; set; } = null!;
    }
}
