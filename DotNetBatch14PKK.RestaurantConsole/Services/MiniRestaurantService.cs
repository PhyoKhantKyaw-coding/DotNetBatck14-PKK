using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBatch14PKK.RestaurantConsole.Interface;
using DotNetBatch14PKK.RestaurantConsole.Models;

namespace DotNetBatch14PKK.RestaurantConsole.Services;

public class MiniRestaurantService
{
    private readonly IRestaurantAPI _restaurantApi;
     
    public MiniRestaurantService()
    {
        _restaurantApi = RestService.For<IRestaurantAPI>("https://localhost:7138");
    }
    public async Task<ResponseModel> CreateOrder(int orderCode, List<Itemlist> items)
    {
        return await _restaurantApi.CreateOrder(orderCode, items);
    }
    public async Task<List<MenuItem>> GetMenuItems()
    {
        return await _restaurantApi.GetMenuItems();
    }
    public async Task<MenuItem> GetMenuItem(int menuItemCode)
    {
        return await _restaurantApi.GetMenuItem(menuItemCode);
    }
    public async Task<List<Order>> GetOrders()
    {
        return await _restaurantApi.GetOrders();
    }
    public async Task<Order> GetOrder(int id)
    {
        return await _restaurantApi.GetOrder(id);
    }
    public async Task<List<OrderItem>> GetOrderItems()
    {
        return await _restaurantApi.GetOrderItems();
    }
    public async Task<ResponseModel> CreateMenuItem(MenuItem menuItem)
    {
        return await _restaurantApi.CreateMenuItem(menuItem);
    }

}
