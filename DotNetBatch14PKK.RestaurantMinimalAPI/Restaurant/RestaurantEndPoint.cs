using DotNetBatch14PKK.RestaurantDomain;
using DotNetBatch14PKK.RestaurantDataBase;
using Microsoft.Identity.Client;

namespace DotNetBatch14PKK.RestaurantMinimalAPI.Restaurant;

public static class RestaurantEndPoint
{ 
    public static IEndpointRouteBuilder MapRestaurantEndPoint(this IEndpointRouteBuilder app)
    {
        MiniRestaurantService service = new MiniRestaurantService();
        
        app.MapPost("/api/Restaurant/CreateMenuItem", (MenuItem requestModel) =>
        {
            var result = service.CreateMenuItem(requestModel);
            return Results.Ok(result);
        })
        .WithName("CreateMenuItem")
        .WithOpenApi();
        app.MapGet("/api/Restaurant/GetMenuItems", () =>
        {
            var result = service.GetMenuItems();
            return Results.Ok(result);
        })
        .WithName("GetMenuItems")
        .WithOpenApi();
        app.MapGet("/api/Restaurant/GetMenuItem", (int id) =>
        {
            var result = service.GetMenuItem(id);
            return Results.Ok(result);
        })
        .WithName("GetMenuItem")
        .WithOpenApi();
        app.MapGet("/api/Restaurant/GetOrders", () =>
        {
            var result = service.GetOrders();
            return Results.Ok(result);
        })
        .WithName("GetOrders")
        .WithOpenApi();
        app.MapGet("/api/Restaurant/GetOrder", (int id) =>
        {
            var result = service.GetOrder(id);
            return Results.Ok(result);
        })
        .WithName("GetOrder")
        .WithOpenApi();
        app.MapPost("/api/Restaurant/CreateOrder", (int OrderCode, List<Itemlist> items) =>
        {
            var result = service.CreateOrder(OrderCode, items);
            return Results.Ok(result);
        })
        .WithName("CreateOrder")
        .WithOpenApi();
        app.MapGet("/api/Restaurant/GetOrderItems", (int OrderCode) =>
        { 
            var result = service.GetOrderItems(OrderCode);
            return Results.Ok(result);
        })
        .WithName("GetOrderItems")
        .WithOpenApi();
        return app;
    }
}
