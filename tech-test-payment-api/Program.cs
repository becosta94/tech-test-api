using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using tech_test_payment_api.Contexts;
using tech_test_payment_api.Interfaces;
using tech_test_payment_api.Repository;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder.Services);

// Add services to the container.
builder.Services.AddDbContext<SalesContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});
builder.Services.AddDbContext<SellersContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddScoped<SalesContext>();
builder.Services.AddScoped<SellersContext>();
builder.Services.AddScoped<ISaleUpdateStatus, SalesRepository>();
builder.Services.AddScoped<ISaleFinder, SalesRepository>();
builder.Services.AddScoped<ISaleFactory, SalesRepository>();



builder.Services.AddControllers().AddJsonOptions(options =>
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
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

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<ISaleFactory, SalesRepository>();
    services.AddTransient<ISaleFinder, SalesRepository>();
    services.AddTransient<ISaleUpdateStatus, SalesRepository>();

}