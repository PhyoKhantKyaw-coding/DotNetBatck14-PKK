using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TravelService _travelService;

        public UserController(TravelService travelService)
        {
            _travelService = travelService;
        }

        [HttpPost("bookings")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingRequestModel request)
        {
            try
            {
                var response = await _travelService.CreateBooking(request);
                return response.Success == "true" ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        [HttpGet("{id}/bookings")]
        public async Task<IActionResult> GetUserBookings(string id)
        {
            try
            {
                var bookings = await _travelService.GetBookings();
                return Ok(bookings.Where(b => b.UserId == id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        [HttpGet("travelers/bookingid")]
        public async Task<IActionResult> GetTravelerbyBookingid(string id)
        {
            try
            {
                var travelers = await _travelService.GetTravelersbyBookingid(id);
                return Ok(travelers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        [HttpPost("bookings/add-traveler")]
        public async Task<IActionResult> AddTraveler([FromBody] TravelerModel traveler)
        {
            try
            {
                var response = await _travelService.AddTraveler(traveler);
                return response.Success == "true" ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        [HttpDelete("bookings/remove-traveler/{traveler_id}")]
        public async Task<IActionResult> RemoveTraveler(string traveler_id)
        {
            try
            {
                var response = await _travelService.RemoveTraveler(traveler_id);
                return response.Success == "true" ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        [HttpGet("bookings/{id}/invoice")]
        public async Task<IActionResult> GetInvoice(string id)
        {
            try
            {
                var booking = await _travelService.GetBookingbyid(id);
                if (booking == null)
                {
                    return NotFound(new { Success = "false", Message = "Booking not found" });
                }
                return Ok(new { Success = "true", BookingId = booking.Id, TotalPrice = booking.TotalPrice });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        [HttpPost("payments")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestModel payment)
        {
            try
            {
                var response = await _travelService.CreatePayment(payment);
                return response.Success == "true" ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }
        [HttpGet("payments")]
        public async Task<IActionResult> GetallPayment()
        {
            try
            {
                var payment = await _travelService.GetPayments();
                return payment != null ? Ok(payment) : NotFound(new { Success = "false", Message = "Payment not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }

        [HttpGet("payments/userid")]
        public async Task<IActionResult> GetPaymentDetails(string id)
        {
            try
            {
                var payment = await _travelService.GetPaymentbyUserid(id);
                return payment != null ? Ok(payment) : NotFound(new { Success = "false", Message = "Payment not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }
        [HttpGet("payments/bookingid")]
        public async Task<IActionResult> GetPaymentDetailsbyBookingid(string id)
        {
            try
            {
                var payment = await _travelService.GetPaymentbyBookingid(id);
                return payment != null ? Ok(payment) : NotFound(new { Success = "false", Message = "Payment not found" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = "false", Message = ex.Message });
            }
        }
    }
}
