using SupermarketAPI.Domain.Interfaces.Repositories;
using SupermarketAPI.Domain.Interfaces.Services;
using SupermarketAPI.Domain.Services;
using SupermarketAPI.Infra.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddTransient<IProdutosService, ProdutosService>();
builder.Services.AddTransient<ICategoriasService, CategoriasService>();

builder.Services.AddTransient<IProdutosRepository, ProdutosRepository>();
builder.Services.AddTransient<ICategoriasRepository, CategoriasRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
