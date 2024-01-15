using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using PersonServices.Context;
using PersonServices.Interfaces;
using PersonServices.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var conn = System.Environment.GetEnvironmentVariable("ConnectionString");
builder.Services.AddDbContext<PgDbContext>(options=>
        options.UseNpgsql(conn),
        ServiceLifetime.Singleton);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IContactInfoService, ContactInfoService>();
//builder.Services.AddScoped<MqttClientService>();
builder.Services.AddSingleton<IMqttClientService,MqttClientService>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var app = builder.Build();

// MyService'in örneğini al
var myService = app.Services.GetService<IMqttClientService>();

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
