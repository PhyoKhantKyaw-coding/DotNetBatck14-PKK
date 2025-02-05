using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly TravelService _travelService;

    public AdminController(TravelService travelService)
    {
        _travelService = travelService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _travelService.GetUsers();
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = "false", Message = ex.Message });
        }
    }

    [HttpGet("bookings")]
    public async Task<IActionResult> GetBookings()
    {
        try
        {
            var bookings = await _travelService.GetBookings();
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = "false", Message = ex.Message });
        }
    }

    [HttpGet("payments")]
    public async Task<IActionResult> GetPayments()
    {
        try
        {
            // Ensure this method exists in TravelService
            var payments = await _travelService.GetPayments();
            return Ok(payments);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = "false", Message = ex.Message });
        }
    }

    [HttpPost("travel-packages")]
    public async Task<IActionResult> CreateTravelPackage(TravelPackageModel model)
    {
        try
        {
            var response = await _travelService.CreateTravelPackage(model);
            return response.Success == "true" ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = "false", Message = ex.Message });
        }
    }

    [HttpPatch("travel-packages/{id}/activate")]
    public async Task<IActionResult> ActivateTravelPackage(String id)
    {
        try
        {
            var response = await _travelService.ActivateTravelPackage(id);
            return response.Success == "true" ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = "false", Message = ex.Message });
        }
    }

    [HttpPatch("travel-packages/{id}/deactivate")]
    public async Task<IActionResult> DeactivateTravelPackage(String id)
    {
        try
        {
            var response = await _travelService.DeactivateTravelPackage(id);
            return response.Success == "true" ? Ok(response) : BadRequest(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Success = "false", Message = ex.Message });
        }
    }
}