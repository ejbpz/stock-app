using Microsoft.EntityFrameworkCore;
using StockApp.Contracts;
using StockApp.Models;
using StockApp.Models.Options;
using StockApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();

builder.Services.AddDbContext<StocksMarketDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", "Rotativa");

app.MapControllers();

app.Run();
