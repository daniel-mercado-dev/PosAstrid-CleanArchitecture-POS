using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Repositories;
using PosAstrid.Infrastructure.Persistence;
using PosAstrid.Infrastructure.Repositories;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<AppDbContext>());


builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

builder.Services.AddScoped<IInventarioRepository, InventarioRepository>();

builder.Services.AddScoped<IVentaRepository, VentaRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.UtcNow.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");
app.MapControllers();  

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}