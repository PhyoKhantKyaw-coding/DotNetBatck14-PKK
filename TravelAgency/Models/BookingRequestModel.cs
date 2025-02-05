namespace TravelAgency.Models
{
    public class BookingRequestModel
    {
        public string UserId { get; set; } = null!;
        public string TravelPackageId { get; set; } = null!;
        // Fixed: Use a List or an Array
        public List<TravelerRequestModel> Travelers { get; set; } = new List<TravelerRequestModel>();
    }

    public class TravelerRequestModel
    {
        public string Name { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
    }
}
