using Microsoft.EntityFrameworkCore;
using PosAstrid.Domain.Abstractions;
using PosAstrid.Domain.Repositories;
using PosAstrid.Infrastructure.Persistence;
using PosAstrid.Infrastructure.Repositories;
using PosAstrid.Infrastructure.Printing;



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


var modoImpresion = builder.Configuration["ModoImpresion"] ?? "Asincrono";

builder.Services.AddScoped<IImpresoraTermica, ImpresoraSimulada>();

if (modoImpresion == "Sincrono")
{
    builder.Services.AddScoped<ITicketPrintQueue, TicketPrintSincrono>();
}
else
{
    builder.Services.AddSingleton<TicketPrintQueue>();
    builder.Services.AddSingleton<ITicketPrintQueue>(sp => sp.GetRequiredService<TicketPrintQueue>());
    builder.Services.AddHostedService<TicketPrintWorker>();
}

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AppDbContext).Assembly);
});


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();  
app.Run();