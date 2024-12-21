var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


EmojiResponseModel Response = null;

app.MapGet("/emojis", async () =>
{
    await FetchAsync();
    return Results.Ok(Response!.Emojis);
})
.WithName("GetEmojis")
.WithOpenApi();

app.MapGet("/emojis/{FilterbyName}", async (string name) =>
{
    await FetchAsync();
    var lst = Response!
        .Emojis
        .Where(x => x.Name.ToLower().Contains(name.ToLower()));
    return Results.Ok(lst);
})
.WithName("GetEmoji")
.WithOpenApi();

async Task FetchAsync()
{
    if (Response is null)
    {
        HttpClient client = new HttpClient();
        var response = await client.GetFromJsonAsync<EmojiResponseModel>("https://gist.githubusercontent.com/oliveratgithub/0bf11a9aff0d6da7b46f1490f86a71eb/raw/d8e4b78cfe66862cf3809443c1dba017f37b61db/emojis.json");
        Response = response!;
    }
}
app.Run();
public class EmojiResponseModel
{
    public Emoji[] Emojis { get; set; }
}
public class Emoji
{
    public string emoji { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Unicode { get; set; }
    public string Html { get; set; }
    public string Category { get; set; }
    public string Order { get; set; }
}



