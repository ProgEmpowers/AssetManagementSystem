using AssetManagementSystem.Context;
using AssetManagementSystem.Mappings;
using AssetManagementSystem.Services.AssetServices;
using AssetManagementSystem.Services.VendorServices;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("LogsAssetManegementSystem/Log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddScoped<IAssetService, AssetService>();
builder.Services.AddScoped<IVendorService,VendorService>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AssetManagementDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbstring")));

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

