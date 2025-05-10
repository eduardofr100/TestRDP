using Microsoft.EntityFrameworkCore;
using Test_API.Repositories.Interfaces;
using Test_API.Repositories;
using Test_API.Services;
using Test_API.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//Se generar instancia de conexion a db
builder.Services.AddDbContext<TestRdpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();
builder.Services.AddScoped<IFacturaRepository, FacturaRepository>();

// Services
builder.Services.AddScoped<PersonaService>();
builder.Services.AddScoped<FacturaService>();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowWPF", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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

app.UseCors("AllowWPF");

app.MapControllers();

app.Run();
