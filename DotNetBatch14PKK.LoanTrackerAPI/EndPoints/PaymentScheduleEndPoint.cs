using DotNetBatch14PKK.LoanTrackerDomain;

namespace DotNetBatch14PKK.LoanTrackerAPI.EndPoints
{
    public static class PaymentScheduleEndPoint
    {
        public static IEndpointRouteBuilder MapPaymentScheduleEndPoint(this IEndpointRouteBuilder app)
        {
            PaymentScheduleService paymentScheduleService = new();
            app.MapGet("/api/paymentschedule", (String loanid) =>
            {
                var paymentSchedules = paymentScheduleService.GetYearlyPaymentSchedule(loanid);
                if (paymentSchedules == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(paymentSchedules);
            });
            app.MapGet("/api/paymentschedule/{id}", (string id,int year) =>
            {
                var paymentSchedules = paymentScheduleService.GetMonthlyPaymentSchedule(id, year);
                if (paymentSchedules == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(paymentSchedules);
            });

            return app;
        }
    }
}
