using DotNetBatch14PKK.RestaurantDomain;
using DotNetBatch14PKK.RestaurantDataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DotNetBatch14PKK.RestaurantRestApI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RestaurantController : ControllerBase
{
    private readonly MiniRestaurantService _service; 
    public RestaurantController()
    {
        _service = new MiniRestaurantService();
    }
    [HttpPost("CreateMenuItem")]
    public IActionResult CreateMenuItem(MenuItem requestModel)
    {
        var result = _service.CreateMenuItem(requestModel);
        return Ok(result);
    }
    [HttpGet("GetMenuItems")]
    public IActionResult GetMenuItems()
    {
        var result = _service.GetMenuItems();
        return Ok(result);
    }
    [HttpGet("GetMenuItem")]
    public IActionResult GetMenuItem(int menuItemCode)
    {
        var result = _service.GetMenuItem(menuItemCode);
        return Ok(result);
    }
    [HttpGet("GetOrders")]
    public IActionResult GetOrders()
    {
        var result = _service.GetOrders();
        return Ok(result);
    }
    [HttpGet("GetOrder")]
    public IActionResult GetOrder(int id)
    {
        var result = _service.GetOrder(id);
        return Ok(result);
    }
    [HttpPost("CreateOrder")]
    public IActionResult CreateOrder(int orderCode, List<Itemlist> items)
    {
        var result = _service.CreateOrder(orderCode, items);
        return Ok(result);
    }

    [HttpGet("GetOrderItems")]
    public IActionResult GetOrderItems(int orderCode)
    {
        var result = _service.GetOrderItems(orderCode);
        return Ok(result);
    }

}
