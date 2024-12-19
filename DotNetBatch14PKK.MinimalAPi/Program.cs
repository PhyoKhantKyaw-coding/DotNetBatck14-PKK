using DotNet_Batch14PKK.Share;
using DotNetBatch14PKK.MinimalAPi.Features.Blog;
using Microsoft.AspNetCore.Mvc;
using BlogModel = DotNet_Batch14PKK.Share.BlogModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();
//app.MapGet("/api/Blog", () =>
//{
//    EfcoreSerives blogService = new EfcoreSerives();
//    var models = blogService.GetBlogs();
//    return Results.Ok(models);
//});
//app.MapPost("/api/Blog/{id}", (string id) =>
//{
//    EfcoreSerives blogService = new EfcoreSerives();
//    var model = blogService.GetBlog(id);
//    return Results.Ok(model);
//});
//app.MapPatch("/api/Blog/{id}", static (string id,BlogModel blog) =>
//{
//    blog.BlogId = id;
//    EfcoreSerives blogService = new EfcoreSerives();
//    var model = blogService.UpdateBlog(blog);
//    return Results.Ok(model);
//});
//app.MapDelete("/api/Blog/{id}", static (string id) =>
//{
//    EfcoreSerives blogService = new EfcoreSerives();
//    var model = blogService.DeleteBlog(id);
//    return Results.Ok(model);
//});
app.MapBlogEndPoint();
//app.MapBlogEndPoint().UseUserEndPoint();
app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
