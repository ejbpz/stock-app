using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
using StockApp.Models;
using StockApp.Services;
using StockApp.Contracts;
using StockApp.Middlewares;
using StockApp.Repositories;
using StockApp.Models.Options;
using StockApp.RepositoryContracts;
using StockApp.Filters.ActionFilters;

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

builder.Services.AddScoped<IFinnhubSearcherService, FinnhubSearcherService>();
builder.Services.AddScoped<IFinnhubGetterService, FinnhubGetterService>();

builder.Services.AddScoped<IStocksGetterService, StocksGetterService>();
builder.Services.AddScoped<IStocksAdderService, StocksAdderService>();

builder.Services.AddTransient<CreateOrderActionFilter>();

if(!builder.Environment.IsEnvironment("Test"))
{
    builder.Services.AddDbContext<StocksMarketDbContext>(
     options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
}


var app = builder.Build();

if(builder.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
{
    app.UseExceptionHandler("/error");
    app.UseExceptionHandlingMiddleware();
}


if (!builder.Environment.IsEnvironment("Test"))
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