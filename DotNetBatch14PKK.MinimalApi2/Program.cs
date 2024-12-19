using DotNetBatch14PKK.MinimalApi2;
using DotNetBatch14PKK.MinimalApi2.Features.Emoji;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContent>();
builder.Services.AddScoped<EmojiService>();
builder.Services.AddHttpClient();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapEmojiEndpoints();
app.Run();
