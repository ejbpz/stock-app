using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;
using StockApp.Contracts;
using StockApp.Filters.ActionFilters;
using StockApp.Models;
using StockApp.Models.Options;
using StockApp.Repositories;
using StockApp.RepositoryContracts;
using StockApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Logging - Serilog
builder.Services.Configure<TradingOptions>(builder.Configuration.GetSection("TradingOptions"));

builder.Host.UseSerilog((HostBuilderContext context, IServiceProvider services, LoggerConfiguration configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services);
});

// Views to Controller
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

// Services
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.RequestProperties | HttpLoggingFields.ResponsePropertiesAndHeaders;
});

builder.Services.AddScoped<IFinnhubRepository, FinnhubRepository>();
builder.Services.AddScoped<IStocksRepository, StocksRepository>();
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();

builder.Services.AddTransient<CreateOrderActionFilter>();

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

app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

public partial class Program { }