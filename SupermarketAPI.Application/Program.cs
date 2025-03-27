using Scalar.AspNetCore;
using SupermarketAPI.Application.Configurations;
using UsersAPI.Application.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

builder.Services.AddDependencyInjection();
builder.Services.AddCorsConfiguration();
//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    #region Swagger

    app.UseSwaggerConfiguration();

    #endregion

    #region Scalar

    app.MapScalarApiReference(options => {
        options.WithTitle("SupermarketAPI")
               .WithTheme(ScalarTheme.Mars)
               .WithDefaultHttpClient(
                    ScalarTarget.CSharp,
                    ScalarClient.HttpClient);        
    });

    #endregion
}

app.UseCorsConfiguration();

app.UseAuthorization();
app.MapControllers();
app.Run();

// Definindo a classe Program.cs como publica
public partial class Program { }