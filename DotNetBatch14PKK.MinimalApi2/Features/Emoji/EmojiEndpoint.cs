using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DotNetBatch14PKK.MinimalApi2.Features.Emoji
{
    public static class EmojiEndpoint
    {
        public static void MapEmojiEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/emojis/insert", async (EmojiService emojiService) =>
            {
                try
                {
                    await emojiService.InsertEmojisAsync();
                    return Results.Ok(new { IsSuccess = true, Message = "Emojis inserted successfully." });
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { IsSuccess = false, Message = ex.Message });
                }
            })
            .WithName("InsertEmojis")
            .WithOpenApi();

            app.MapGet("/api/emojis", async (EmojiService emojiService) =>
            {
                try
                {
                    var response = await emojiService.GetEmojisAsync();
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { IsSuccess = false, Message = ex.Message });
                }
            })
            .WithName("GetAllEmojis")
            .WithOpenApi();

            app.MapGet("/api/emojis/{id}", async (int id, EmojiService emojiService) =>
            {
                try
                {
                    var response = await emojiService.GetEmojiByIdAsync(id);
                    if (!response.IsSuccess)
                        return Results.NotFound(response);
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { IsSuccess = false, Message = ex.Message });
                }
            })
            .WithName("GetEmojiById")
            .WithOpenApi();

            app.MapGet("/api/emojis/filter", async (string name, EmojiService emojiService) =>
            {
                try
                {
                    var response = await emojiService.FilterByNameAsync(name);
                    return Results.Ok(response);
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(new { IsSuccess = false, Message = ex.Message });
                }
            })
            .WithName("FilterEmojisByName")
            .WithOpenApi();

           
        }
    }
}
