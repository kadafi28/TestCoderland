using DataEF;
using DataEF.Repositories;
using DataEF.Repositories.CatalogoRepo;
using Dominio.Interfaces;
using Dominio.Interfaces.IModelos;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Contexto

// Configuración del contexto de base de datos.
// Se registra el servicio DbContext utilizando PostgreSQL como proveedor,
// y se obtiene la cadena de conexión desde la configuración de la aplicación.
// También se registra el UnitOfWork como un servicio de alcance (Scoped) para manejar transacciones.
builder.Services.AddDbContext<Contexto>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

#endregion

#region Repositories

// Registro de repositorios específicos como servicios Scoped.
// Esto asegura que cada solicitud tenga su propia instancia del repositorio.
builder.Services.AddScoped<IMarcaAutosRepository, MarcaAutosRepository>();

#endregion

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
});

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<Contexto>();
//    dbContext.Database.Migrate();
//}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAllOrigins");

app.Run();
