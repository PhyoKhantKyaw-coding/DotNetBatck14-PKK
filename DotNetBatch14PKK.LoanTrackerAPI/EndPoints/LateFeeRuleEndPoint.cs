using DotNetBatch14PKK.LoanTrackerDatabase;
using DotNetBatch14PKK.LoanTrackerDomain;

namespace DotNetBatch14PKK.LoanTrackerAPI.EndPoints
{
    public static class LateFeeRuleEndPoint
    {
        public static IEndpointRouteBuilder MapLateFeeRuleEndPoint(this IEndpointRouteBuilder app)
        {
            LateFeeRuleService lateFeeRuleService = new();
            app.MapPost("/api/latefeerule", (LateFeeRuleModel lateFeeRule) =>
            {
                var lateFee = lateFeeRuleService.CreateLateFeeRule(lateFeeRule);
                return Results.Ok(lateFee);
            });
            app.MapGet("/api/latefeerule", () =>
            {
                var lateFees = lateFeeRuleService.GetLateFeeRules();
                return Results.Ok(lateFees);
            });
            app.MapGet("/api/latefeerule/{id}", (string id) =>
            {
                var lateFee = lateFeeRuleService.GetLateFeeRuleById(id);
                if (lateFee == null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(lateFee);
            });
            app.MapPut("/api/latefeerule/{id}", (string id, int MinDaysOverdue, int MaxDaysOverdue, decimal LateFeeAmount) =>
            {
                var lateFee = lateFeeRuleService.UpdateLateFeeRule(id,MinDaysOverdue,MaxDaysOverdue,LateFeeAmount);
                return Results.Ok(lateFee);
            });
            app.MapDelete("/api/latefeerule/{id}", (string id) =>
            {
                var lateFee = lateFeeRuleService.DeleteLateFeeRule(id);
                return Results.Ok(lateFee);
            });
            return app;
        }
    }
}
