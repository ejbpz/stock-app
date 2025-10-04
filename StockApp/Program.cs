using Microsoft.EntityFrameworkCore;
using StockApp.Models;
using StockApp.Services;
using StockApp.Contracts;
using StockApp.Repositories;
using StockApp.Models.Options;
using StockApp.RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
builder.Services.AddScoped<IStocksRepository, StocksRepository>();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();

if(!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<StocksMarketDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
}


var app = builder.Build();

if(builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


if(!builder.Environment.IsEnvironment("Test"))
{
    Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", "Rotativa");
}

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { }