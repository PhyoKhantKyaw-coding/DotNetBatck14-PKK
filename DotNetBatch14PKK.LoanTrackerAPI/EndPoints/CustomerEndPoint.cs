using DotNetBatch14PKK.LoanTrackerDatabase;
using DotNetBatch14PKK.LoanTrackerDomain;

namespace DotNetBatch14PKK.LoanTrackerAPI.EndPoints;

public static class CustomerEndPoint
{
    public static IEndpointRouteBuilder MapCustomerEndPoint(this IEndpointRouteBuilder app)
    {
        CustomerService customerService = new();
        app.MapGet("/api/customers",  () =>
        {
            var customers = customerService.GetCustomers();
            return Results.Ok(customers);
        });
        app.MapGet("/api/customers/{id}", (string id) =>
        {
            var customer = customerService.GetCustomerById(id);
            if (customer == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(customer);
        });
        app.MapPost("/api/customers", (CustomerModel customer) =>
        {
            var Customer = customerService.CreateCustomer(customer);
            return Results.Ok(Customer);
        });
        
        return app;
    }
}
