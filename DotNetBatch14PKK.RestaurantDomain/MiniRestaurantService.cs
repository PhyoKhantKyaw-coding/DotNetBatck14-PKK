using DotNetBatch14PKK.RestaurantDataBase;
using Microsoft.EntityFrameworkCore;


namespace DotNetBatch14PKK.RestaurantDomain;

public class MiniRestaurantService
{
    private readonly AppDbContent _db;
    public MiniRestaurantService()
    {
        _db = new AppDbContent();
    }
     
    public ResponseModel CreateMenuItem(MenuItem requestModel)
    {
        _db.Mitem.Add(requestModel);
        var result = _db.SaveChanges();
        string message = result > 0 ? "Saving successful." : "Saving failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
    public List<MenuItem> GetMenuItems()
    {
        var lst = _db.Mitem.AsNoTracking().ToList();
        return lst;
    }
    public MenuItem GetMenuItem(int menuItemCode)
    {
        var item = _db.Mitem.AsNoTracking().FirstOrDefault(x => x.MenuItemCode == menuItemCode);
        return item!;
    }

    public List<Order> GetOrders()
    {
        var lst = _db.Order.AsNoTracking().ToList();
        return lst;
    }

    public Order GetOrder(int id)
    {
        var item = _db.Order.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return item!;
    }

    public ResponseModel CreateOrder(int OrderCode, List<Itemlist> items)
    {
        decimal totalPrice = 0;
        Order order = new()
        {
            OrderCode = OrderCode,
        };
        foreach (var item in items)
        {
            var Mitem = _db.Mitem.AsNoTracking().FirstOrDefault(x => x.MenuItemCode == item.MenuItemCode);
            OrderItem orderItem = new()
            {
                MenuItemCode = item.MenuItemCode,
                OrderCode = order.OrderCode,
                Quantity = item.Quantity,
                TotalPrice = Mitem.Price * item.Quantity
            };
            _db.OrderItem.Add(orderItem);
            _db.SaveChanges();
            totalPrice += orderItem.TotalPrice;
        }
        order.TotalPrice = totalPrice;
        _db.Order.Add(order);
        var result = _db.SaveChanges();
        string message = result > 0 ? "Saving successful." : "Saving failed.";
        ResponseModel model = new()
        {
            IsSuccessful = result > 0,
            Message = message
        };
        return model;
    }
    public List<OrderItem> GetOrderItems(int orderCode)
    {
        var lst = _db.OrderItem.AsNoTracking().Where(x => x.OrderCode == orderCode).ToList();
        return lst;
    }


}
