using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Helpers;
using Vehicles_API.interfaces;
using Vehicles_API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<VehicleContext>(options => 
options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));
// Dependecy injection för våra egna interface och klasser
// builder.Services.AddScoped<Interface, konkret klass som implementerar föregående interface>

builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IManufacturerRepository, ManufacturerRepository>();

//  add automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers();
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
