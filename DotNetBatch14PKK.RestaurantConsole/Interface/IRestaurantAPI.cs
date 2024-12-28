using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetBatch14PKK.RestaurantConsole.Models;

namespace DotNetBatch14PKK.RestaurantConsole.Interface;

internal interface IRestaurantAPI
{
    [Get("/api/Restaurant/GetMenuItems")]
    Task<List<MenuItem>> GetMenuItems(); 

    [Post("/api/Restaurant/CreateMenuItem")]
    Task<ResponseModel> CreateMenuItem(MenuItem menuItem);

    [Get("/api/Restaurant/GetMenuItem")]
    Task<MenuItem> GetMenuItem(int menuItemCode);

    [Post("/api/Restaurant/CreateOrder")]
    Task<ResponseModel> CreateOrder(int ordercode, List<Itemlist> items);

    [Get("/api/Restaurant/GetOrders")]
    Task<List<Order>> GetOrders();

    [Get("/api/Restaurant/GetOrder")]
    Task<Order> GetOrder(int id);

    [Get("/api/Restaurant/GetOrderItems")]
    Task<List<OrderItem>> GetOrderItems();


}
