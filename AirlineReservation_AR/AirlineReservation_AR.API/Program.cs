using AirlineReservation_AR.API.Interfaces;
using AirlineReservation_AR.API.Services.Momo;
using AirlineReservation_AR.API.Services;
using AirlineReservation_AR.src.AirlineReservation.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using AirlineReservation_AR.API.Services.Momo.Configurations;


var builder = WebApplication.CreateBuilder(args);

// 1. Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB
builder.Services.AddDbContext<AirlineReservationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AirlineReservationDatabase")));

var momoConfig = new MomoConfiguration();
builder.Configuration.GetSection("Momo").Bind(momoConfig);

builder.Services.AddSingleton(momoConfig);
builder.Services.AddScoped<MomoServiceAPI>();

builder.Services.AddScoped<IPaymentAPI, PaymentAPIServices>();
builder.Services.AddScoped<IPaymentCallbackService, PaymentCallbackService>();



var app = builder.Build();

// 3. Configure pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 4. Chạy server
app.Run();
