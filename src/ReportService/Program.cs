using Microsoft.EntityFrameworkCore;
using ReportManagementService.Context;
using ReportManagementService.Interfaces;
using ReportManagementService.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var conn = System.Environment.GetEnvironmentVariable("ConnectionString");

builder.Services.AddDbContext<PgDbContext>(options =>
        options.UseNpgsql(conn),
        ServiceLifetime.Singleton);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


builder.Services.AddSingleton<IReportService, ReportService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

// MyService'in örneğini al
var myService = app.Services.GetService<IReportService>();

// MyService içindeki DbContext'i kullan
myService.InitMqttClient();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

var dbContext = app.Services.GetRequiredService<PgDbContext>();
dbContext.Database.Migrate();

app.Run();
