using DotNetBatch14PKK.LoanTrackerDatabase;
using DotNetBatch14PKK.LoanTrackerDomain;

namespace DotNetBatch14PKK.LoanTrackerAPI.EndPoints
{
    public static class MortgageLoanEndPoint
    {
        public static IEndpointRouteBuilder MapMortgageLoanEndPoint(this IEndpointRouteBuilder app)
        {
            MortgageLoanService mortgageLoanService = new();
            app.MapPost("/api/mortgageloan", (LoanDTO mortgageLoan) =>
            {
                var Loan = mortgageLoanService.CreateMortgageLoan(mortgageLoan);
                return Results.Ok(Loan);
            });
            app.MapGet("/api/mortgageloan", () =>
            {
                var mortgageLoans = mortgageLoanService.GetMortgageLoans();
                return Results.Ok(mortgageLoans);
            });
            app.MapGet("/api/mortgageloan/{id}", (string id) =>
            {
                var mortgageLoan = mortgageLoanService.GetMortgageLoanById(id);
                if (mortgageLoan == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(mortgageLoan);
            });

            return app;
        }
    }
}
