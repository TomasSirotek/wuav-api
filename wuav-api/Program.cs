using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Mvc;
using wuav_api.Configuration;
using wuav_api.Identity;
using wuav_api.Identity.BindingModels;
using wuav_api.Services.Interface;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterServices();
builder.Configuration.AddDotNetEnv();
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);
});

DotNetEnv.Env.Load();

var app = builder.Build();
app.UseCors("AllowOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


Dictionary<int, List<string>> TempImages = new Dictionary<int, List<string>>(); // temp saving of the files sent from the swift ui app

// THIS ENDPOINT IS FOR SWIFT UI TO UPLOAD IMAGES TO THE SERVER TEMP
app.MapPost("api/users/{userId}/images", async (int userId, IFormFile image) =>
{
    using (var memoryStream = new MemoryStream())
    {
        await image.CopyToAsync(memoryStream);
        var base64Image = Convert.ToBase64String(memoryStream.ToArray());
        if (!TempImages.ContainsKey(userId))
        {
            TempImages[userId] = new List<string>();
        }

        TempImages[userId].Add(base64Image);
    }
    return Results.Ok();
});



// THIS ENDPOINT IS FOR DESKTOP APP UI TO DISPLAY THE IMAGES 
app.MapGet("api/users/{userId}/temp-images", (int userId) =>
{
    if (TempImages.TryGetValue(userId, out var base64Images))
    {
        return Results.Json(base64Images);
    }
    else
    {
        return Results.NotFound("No images found for the given user ID");
    }
});


// THIS ENDPOINT IS FOR DESKTOP APP UI TO DELETE THE IMAGES
app.MapDelete("api/users/{userId}/temp-images", (int userId) =>
{
    if (TempImages.ContainsKey(userId))
    {
        TempImages.Remove(userId);
        return Results.Ok("Images removed for the given user ID");
    }
    else
    {
        return Results.NotFound("No images found for the given user ID");
    }
});

app.Run();


