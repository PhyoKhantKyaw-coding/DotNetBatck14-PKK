using DotNetBatch14PKK.LoanTrackerDatabase;
using DotNetBatch14PKK.LoanTrackerDomain;

namespace DotNetBatch14PKK.LoanTrackerAPI.EndPoints
{
    public static class PaymentEndPoint
    {
        public static IEndpointRouteBuilder MapPaymentEndPoint(this IEndpointRouteBuilder app)
        {
            PaymentService paymentService = new();
            app.MapPost("/api/payment", (PaymentModel payment,String id) =>
            {
                var Payment = paymentService.CreatePayment(payment,id);
                return Results.Ok(Payment);
            });
            app.MapGet("/api/payment", () =>
            {
                var payments = paymentService.GetPayments();
                return Results.Ok(payments);
            });
            app.MapGet("/api/payment/{id}", (string id) =>
            {
                var payment = paymentService.GetPaymentById(id);
                if (payment == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(payment);
            });
            return app;
        }
    }
}
