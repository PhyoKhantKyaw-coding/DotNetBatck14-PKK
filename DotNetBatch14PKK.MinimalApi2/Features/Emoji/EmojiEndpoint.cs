namespace DotNetBatch14PKK.MinimalApi2.Features.Emoji
{
    public static class EmojiEndpoint
    {
        public static void MapEmojiEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/api/emojis/insert", async (EmojiService emojiService) =>
            {
                await emojiService.InsertEmojisAsync();
                return Results.Ok(new { IsSuccess = true, Message = "Emojis inserted successfully." });
            })
            .WithName("InsertEmojis")
            .WithOpenApi();

            app.MapGet("/api/emojis", async (EmojiService emojiService) =>
            {
                var response = await emojiService.GetEmojisAsync();
                return Results.Ok(response);
            })
            .WithName("GetAllEmojis")
            .WithOpenApi();

            app.MapGet("/api/emojis/{id}", async (int id, EmojiService emojiService) =>
            {
                var response = await emojiService.GetEmojiByIdAsync(id);
                if (!response.IsSuccess)
                    return Results.NotFound(response);
                return Results.Ok(response);

            })
            .WithName("GetEmojiById")
            .WithOpenApi();

            app.MapGet("/api/emojis/filter", async (string name, EmojiService emojiService) =>
            {
                var response = await emojiService.FilterByNameAsync(name);
                return Results.Ok(response);
            })
            .WithName("FilterEmojisByName")
            .WithOpenApi();


        }
    }
}
