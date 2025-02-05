namespace TravelAgency.Models
{
    public class PaymentRequestModel
    {
        public string UserId { get; set; } = null!;

        public string BookingId { get; set; } = null!;

        public decimal Amount { get; set; }
    }
}
