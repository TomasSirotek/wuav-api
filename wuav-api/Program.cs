using wuav_api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

var summaries = new[]
{
    "Freezing", "Bracing"
};

app.MapGet("/weatherforecast", () =>
{
    var foreCast = Enumerable
        .Range(1, 6)
        .Select(index => new WeatherForecast(
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return foreCast;
}).WithName("GetWeatherForecast").WithOpenApi();

app.Run();


internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 * (int)(TemperatureC / 0.5556);
}


